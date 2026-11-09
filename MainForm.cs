using System.Drawing.Drawing2D;

namespace TaxiApp;

public partial class MainForm : Form
{
    // ===================== Модель данных =====================

    /// <summary>Тариф такси.</summary>
    private sealed record Tariff(string Name, decimal BaseFare, decimal PricePerKm, bool AllowsPets, string Description);

    /// <summary>Оформленный заказ. Объект кладётся прямо в ListBox, а текст берётся из ToString().</summary>
    private sealed class Order
    {
        public int Number { get; init; }
        public DateTime Time { get; init; }
        public string From { get; init; }
        public string To { get; init; }
        public string TariffName { get; init; }
        public decimal Distance { get; init; }
        public string Options { get; init; }
        public string Payment { get; init; }
        public decimal Total { get; init; }

        // Строка, которая отображается в ListBox
        public override string ToString() =>
            $"№{Number} | {Time:HH:mm} | {From} → {To} | {TariffName} | {Total:N0} ₽";

        // Подробности для Label под списком
        public string GetDetails() =>
            $"Откуда: {From}   Куда: {To}\n" +
            $"Тариф: {TariffName}, {Distance:0.#} км   Опции: {Options}\n" +
            $"Оплата: {Payment}   Итого: {Total:N0} ₽";

        // Проверка на дубликат (защита от двойного нажатия)
        public bool IsSameAs(Order other) =>
            From == other.From && To == other.To && TariffName == other.TariffName &&
            Distance == other.Distance && Options == other.Options && Payment == other.Payment;
    }

    private enum StatusKind { Info, Success, Error }

    // ===================== Константы и поля =====================

    private const decimal ChildSeatPrice = 50m;
    private const decimal PetsPrice = 100m;
    private const decimal LuggagePrice = 70m;
    private const decimal CardDiscount = 0.05m;   // скидка 5% при оплате картой

    private static readonly Tariff[] Tariffs =
    {
        new("Эконом",  100m, 20m, true,  "Эконом: подача 100 ₽ + 20 ₽/км. Бюджетные автомобили."),
        new("Комфорт", 150m, 28m, true,  "Комфорт: подача 150 ₽ + 28 ₽/км. Просторный салон, кондиционер."),
        new("Бизнес",  250m, 40m, false, "Бизнес: подача 250 ₽ + 40 ₽/км. Премиум-авто. Перевозка животных недоступна.")
    };

    private static readonly Color InvalidBackColor = Color.MistyRose;

    private int orderCounter = 1;
    private readonly Font boldFont;
    private readonly Font regularFont;

    // ===================== Инициализация =====================

    public MainForm()
    {
        InitializeComponent();

        regularFont = Font;
        boldFont = new Font(Font, FontStyle.Bold);

        Icon = CreateAppIcon();
        InitializeTariffs();
        InitializeToolTips();
        ResetForm();
        ShowStatus("Заполните форму и нажмите «Вызвать такси».", StatusKind.Info);
        UpdateOrderSelectionState();
    }

    /// <summary>Заполняет ComboBox тарифами.</summary>
    private void InitializeTariffs()
    {
        cmbTariff.DataSource = Tariffs;
        cmbTariff.DisplayMember = nameof(Tariff.Name);
        cmbTariff.SelectedIndex = 0;
    }

    /// <summary>Подсказки ToolTip для элементов управления.</summary>
    private void InitializeToolTips()
    {
        toolTip.SetToolTip(txtFrom, "Введите адрес, откуда нужно забрать пассажира");
        toolTip.SetToolTip(txtTo, "Введите адрес, куда нужно доехать");
        toolTip.SetToolTip(numDistance, "Примерное расстояние поездки в километрах");
        toolTip.SetToolTip(cmbTariff, "Выберите тариф: от него зависит стоимость и доступные опции");
        toolTip.SetToolTip(chkChildSeat, "Автомобиль будет оборудован детским креслом");
        toolTip.SetToolTip(chkPets, "Недоступно в тарифе «Бизнес»");
        toolTip.SetToolTip(chkLuggage, "Для крупного багажа (чемоданы, коляски)");
        toolTip.SetToolTip(rbCash, "Оплата водителю наличными");
        toolTip.SetToolTip(rbCard, "Оплата банковской картой со скидкой 5%");
        toolTip.SetToolTip(btnOrder, "Оформить заказ и добавить его в список (Enter)");
        toolTip.SetToolTip(btnClear, "Сбросить все поля формы (список заказов сохраняется)");
        toolTip.SetToolTip(btnDelete, "Удалить выбранный заказ из списка (Delete)");
        toolTip.SetToolTip(lstOrders, "Выберите заказ, чтобы увидеть подробности. Правая кнопка — удалить");
    }

    /// <summary>Рисует иконку приложения (жёлтый круг с буквой «Т») без внешних файлов.</summary>
    private static Icon CreateAppIcon()
    {
        using var bitmap = new Bitmap(32, 32);
        using (var g = Graphics.FromImage(bitmap))
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.Transparent);
            g.FillEllipse(Brushes.Gold, 2, 2, 28, 28);
            using var pen = new Pen(Color.Black, 2);
            g.DrawEllipse(pen, 2, 2, 28, 28);
            using var font = new Font("Segoe UI", 13F, FontStyle.Bold);
            g.DrawString("T", font, Brushes.Black, 7, 4);
        }
        return Icon.FromHandle(bitmap.GetHicon());
    }

    // ===================== Обработчики событий =====================

    /// <summary>Кнопка «Вызвать такси»: проверка данных и добавление заказа в ListBox.</summary>
    private void btnOrder_Click(object sender, EventArgs e)
    {
        if (!ValidateForm())
            return;

        Order order = CreateOrder();

        // Защита от двойного нажатия / повторной отправки одинакового заказа
        foreach (Order existing in lstOrders.Items)
        {
            if (existing.IsSameAs(order))
            {
                ShowStatus($"Такой заказ уже оформлен (№{existing.Number}). Повторный не создан.", StatusKind.Error);
                lstOrders.SelectedItem = existing;
                return;
            }
        }

        orderCounter++;
        lstOrders.Items.Add(order);
        lstOrders.SelectedItem = order;
        ShowStatus($"Такси вызвано! Заказ №{order.Number}, стоимость {order.Total:N0} ₽.", StatusKind.Success);
    }

    /// <summary>Кнопка «Очистить»: сбрасывает все поля формы в исходное состояние.</summary>
    private void btnClear_Click(object sender, EventArgs e)
    {
        ResetForm();
        ShowStatus("Форма очищена. Список оформленных заказов сохранён.", StatusKind.Info);
    }

    /// <summary>Кнопка «Удалить выбранный» и пункт контекстного меню.</summary>
    private void btnDelete_Click(object sender, EventArgs e) => DeleteSelectedOrder();

    /// <summary>Выбор тарифа: обновляет описание, доступность опции «Животные» и стоимость.</summary>
    private void cmbTariff_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbTariff.SelectedItem is not Tariff tariff)
            return;

        lblTariffInfo.Text = tariff.Description;

        bool petsWereChecked = chkPets.Checked;
        chkPets.Enabled = tariff.AllowsPets;
        if (!tariff.AllowsPets)
        {
            chkPets.Checked = false;
            if (petsWereChecked)
                ShowStatus($"В тарифе «{tariff.Name}» перевозка животных недоступна — опция снята.", StatusKind.Error);
        }

        UpdateTotal();
    }

    /// <summary>Изменение любого CheckBox: меняет внешний вид и пересчитывает стоимость.</summary>
    private void chkOption_CheckedChanged(object sender, EventArgs e)
    {
        if (sender is CheckBox box)
        {
            box.Font = box.Checked ? boldFont : regularFont;
            box.ForeColor = box.Checked ? Color.DarkGreen : SystemColors.ControlText;
        }
        UpdateTotal();
    }

    /// <summary>Изменение формы оплаты (RadioButton): обновляет подсказку и стоимость.</summary>
    private void rbPayment_CheckedChanged(object sender, EventArgs e)
    {
        lblPaymentHint.Text = rbCard.Checked
            ? "Скидка 5% при оплате картой"
            : "Оплата наличными водителю";
        lblPaymentHint.ForeColor = rbCard.Checked ? Color.DarkGreen : Color.DimGray;
        UpdateTotal();
    }

    private void numDistance_ValueChanged(object sender, EventArgs e) => UpdateTotal();

    /// <summary>Потеря фокуса TextBox: валидация в реальном времени (подсветка красным).</summary>
    private void txtAddress_Leave(object sender, EventArgs e)
    {
        if (sender is not TextBox box)
            return;

        string name = box == txtFrom ? "адрес подачи" : "адрес назначения";
        string error = GetAddressError(box, name);
        SetFieldState(box, error == null);
        if (error != null)
            ShowStatus(error, StatusKind.Error);
    }

    /// <summary>Как только пользователь начал исправлять поле — подсветка снимается.</summary>
    private void txtAddress_TextChanged(object sender, EventArgs e)
    {
        if (sender is TextBox box && box.BackColor == InvalidBackColor && box.Text.Trim().Length >= 3)
            SetFieldState(box, true);
    }

    /// <summary>Выбор элемента ListBox: показывает подробности и включает кнопку удаления.</summary>
    private void lstOrders_SelectedIndexChanged(object sender, EventArgs e) => UpdateOrderSelectionState();

    /// <summary>Правая кнопка мыши выбирает элемент под курсором (для контекстного меню).</summary>
    private void lstOrders_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Right)
        {
            int index = lstOrders.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
                lstOrders.SelectedIndex = index;
        }
    }

    /// <summary>Клавиша Delete в списке удаляет выбранный заказ.</summary>
    private void lstOrders_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Delete)
        {
            DeleteSelectedOrder();
            e.Handled = true;
        }
    }

    // ===================== Бизнес-логика =====================

    private Tariff GetSelectedTariff() => (Tariff)cmbTariff.SelectedItem;

    /// <summary>Считает стоимость поездки с учётом тарифа, расстояния, опций и формы оплаты.</summary>
    private decimal CalculateTotal()
    {
        Tariff tariff = GetSelectedTariff();
        decimal total = tariff.BaseFare + tariff.PricePerKm * numDistance.Value;

        if (chkChildSeat.Checked) total += ChildSeatPrice;
        if (chkPets.Checked) total += PetsPrice;
        if (chkLuggage.Checked) total += LuggagePrice;
        if (rbCard.Checked) total *= 1 - CardDiscount;

        return Math.Round(total, 0, MidpointRounding.AwayFromZero);
    }

    private void UpdateTotal()
    {
        if (cmbTariff.SelectedItem == null)
            return;

        lblTotal.Text = $"Ориентировочная стоимость: {CalculateTotal():N0} ₽";
    }

    /// <summary>Формирует текст выбранных опций.</summary>
    private string GetOptionsText()
    {
        var options = new List<string>();
        if (chkChildSeat.Checked) options.Add("детское кресло");
        if (chkPets.Checked) options.Add("животные");
        if (chkLuggage.Checked) options.Add("багаж");

        return options.Count > 0 ? string.Join(", ", options) : "без опций";
    }

    private Order CreateOrder() => new()
    {
        Number = orderCounter,
        Time = DateTime.Now,
        From = txtFrom.Text.Trim(),
        To = txtTo.Text.Trim(),
        TariffName = GetSelectedTariff().Name,
        Distance = numDistance.Value,
        Options = GetOptionsText(),
        Payment = rbCard.Checked ? "карта" : "наличные",
        Total = CalculateTotal()
    };

    private void DeleteSelectedOrder()
    {
        if (lstOrders.SelectedItem is not Order order)
        {
            ShowStatus("Сначала выберите заказ в списке.", StatusKind.Error);
            return;
        }

        int index = lstOrders.SelectedIndex;
        lstOrders.Items.Remove(order);

        // Выделяем соседний элемент, чтобы можно было удалять подряд
        if (lstOrders.Items.Count > 0)
            lstOrders.SelectedIndex = Math.Min(index, lstOrders.Items.Count - 1);

        UpdateOrderSelectionState();
        ShowStatus($"Заказ №{order.Number} удалён.", StatusKind.Info);
    }

    private void UpdateOrderSelectionState()
    {
        bool hasSelection = lstOrders.SelectedItem is Order;
        btnDelete.Enabled = hasSelection;
        miDeleteOrder.Enabled = hasSelection;

        lblDetails.Text = lstOrders.SelectedItem is Order order
            ? order.GetDetails()
            : "Выберите заказ в списке, чтобы увидеть подробности.";
    }

    // ===================== Проверка ввода =====================

    /// <summary>Возвращает текст ошибки для адреса или null, если всё корректно.</summary>
    private static string GetAddressError(TextBox box, string fieldName)
    {
        string text = box.Text.Trim();

        if (text.Length == 0)
            return $"Заполните поле: {fieldName}.";
        if (text.Length < 3)
            return $"Поле «{fieldName}» слишком короткое (минимум 3 символа).";

        return null;
    }

    /// <summary>Подсвечивает поле красным (некорректно) или возвращает обычный цвет.</summary>
    private static void SetFieldState(TextBox box, bool isValid) =>
        box.BackColor = isValid ? SystemColors.Window : InvalidBackColor;

    /// <summary>Полная проверка формы перед оформлением заказа.</summary>
    private bool ValidateForm()
    {
        string fromError = GetAddressError(txtFrom, "адрес подачи");
        string toError = GetAddressError(txtTo, "адрес назначения");
        SetFieldState(txtFrom, fromError == null);
        SetFieldState(txtTo, toError == null);

        if (fromError != null)
        {
            ShowStatus(fromError, StatusKind.Error);
            txtFrom.Focus();
            return false;
        }

        if (toError != null)
        {
            ShowStatus(toError, StatusKind.Error);
            txtTo.Focus();
            return false;
        }

        if (string.Equals(txtFrom.Text.Trim(), txtTo.Text.Trim(), StringComparison.OrdinalIgnoreCase))
        {
            // Критическая ошибка — показываем окно MessageBox
            SetFieldState(txtTo, false);
            MessageBox.Show("Адрес подачи и адрес назначения совпадают.\nУкажите разные адреса.",
                "Ошибка в маршруте", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtTo.Focus();
            return false;
        }

        if (numDistance.Value <= 0)
        {
            ShowStatus("Укажите расстояние поездки больше 0 км.", StatusKind.Error);
            numDistance.Focus();
            return false;
        }

        return true;
    }

    // ===================== Вспомогательные методы =====================

    /// <summary>Показывает сообщение в Label с цветовым выделением.</summary>
    private void ShowStatus(string message, StatusKind kind)
    {
        lblStatus.Text = message;
        switch (kind)
        {
            case StatusKind.Success:
                lblStatus.ForeColor = Color.DarkGreen;
                lblStatus.BackColor = Color.Honeydew;
                break;
            case StatusKind.Error:
                lblStatus.ForeColor = Color.Firebrick;
                lblStatus.BackColor = Color.MistyRose;
                break;
            default:
                lblStatus.ForeColor = Color.DimGray;
                lblStatus.BackColor = SystemColors.Control;
                break;
        }
    }

    /// <summary>Сбрасывает все поля и состояния формы (список заказов не трогает).</summary>
    private void ResetForm()
    {
        txtFrom.Clear();
        txtTo.Clear();
        SetFieldState(txtFrom, true);
        SetFieldState(txtTo, true);

        numDistance.Value = 5;
        cmbTariff.SelectedIndex = 0;

        chkChildSeat.Checked = false;
        chkPets.Checked = false;
        chkLuggage.Checked = false;
        rbCash.Checked = true;

        lblPaymentHint.Text = "Оплата наличными водителю";
        lblPaymentHint.ForeColor = Color.DimGray;

        UpdateTotal();
        txtFrom.Focus();
    }
}
