namespace TaxiApp;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Код, автоматически созданный конструктором форм

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();

        // ---- создание элементов ----
        gbRoute = new GroupBox();
        lblFrom = new Label();
        txtFrom = new TextBox();
        lblTo = new Label();
        txtTo = new TextBox();
        lblDistance = new Label();
        numDistance = new NumericUpDown();
        lblKm = new Label();

        gbTariff = new GroupBox();
        cmbTariff = new ComboBox();
        lblTariffInfo = new Label();

        gbOptions = new GroupBox();
        chkChildSeat = new CheckBox();
        chkPets = new CheckBox();
        chkLuggage = new CheckBox();

        gbPayment = new GroupBox();
        rbCash = new RadioButton();
        rbCard = new RadioButton();
        lblPaymentHint = new Label();

        lblTotal = new Label();
        btnOrder = new Button();
        btnClear = new Button();
        btnDelete = new Button();
        lblStatus = new Label();

        gbOrders = new GroupBox();
        lstOrders = new ListBox();
        lblDetails = new Label();

        ctxOrders = new ContextMenuStrip(components);
        miDeleteOrder = new ToolStripMenuItem();
        toolTip = new ToolTip(components);

        gbRoute.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)numDistance).BeginInit();
        gbTariff.SuspendLayout();
        gbOptions.SuspendLayout();
        gbPayment.SuspendLayout();
        gbOrders.SuspendLayout();
        ctxOrders.SuspendLayout();
        SuspendLayout();

        // ---- gbRoute: группа «Маршрут» ----
        gbRoute.Controls.Add(lblFrom);
        gbRoute.Controls.Add(txtFrom);
        gbRoute.Controls.Add(lblTo);
        gbRoute.Controls.Add(txtTo);
        gbRoute.Controls.Add(lblDistance);
        gbRoute.Controls.Add(numDistance);
        gbRoute.Controls.Add(lblKm);
        gbRoute.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        gbRoute.Location = new Point(12, 12);
        gbRoute.Name = "gbRoute";
        gbRoute.Size = new Size(600, 125);
        gbRoute.TabIndex = 0;
        gbRoute.TabStop = false;
        gbRoute.Text = "Маршрут";

        // lblFrom
        lblFrom.AutoSize = true;
        lblFrom.Location = new Point(15, 29);
        lblFrom.Name = "lblFrom";
        lblFrom.Text = "Адрес подачи:";

        // txtFrom
        txtFrom.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtFrom.Location = new Point(140, 26);
        txtFrom.MaxLength = 100;
        txtFrom.Name = "txtFrom";
        txtFrom.Size = new Size(440, 23);
        txtFrom.TabIndex = 0;
        txtFrom.TextChanged += txtAddress_TextChanged;
        txtFrom.Leave += txtAddress_Leave;

        // lblTo
        lblTo.AutoSize = true;
        lblTo.Location = new Point(15, 62);
        lblTo.Name = "lblTo";
        lblTo.Text = "Адрес назначения:";

        // txtTo
        txtTo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtTo.Location = new Point(140, 59);
        txtTo.MaxLength = 100;
        txtTo.Name = "txtTo";
        txtTo.Size = new Size(440, 23);
        txtTo.TabIndex = 1;
        txtTo.TextChanged += txtAddress_TextChanged;
        txtTo.Leave += txtAddress_Leave;

        // lblDistance
        lblDistance.AutoSize = true;
        lblDistance.Location = new Point(15, 95);
        lblDistance.Name = "lblDistance";
        lblDistance.Text = "Расстояние:";

        // numDistance
        numDistance.DecimalPlaces = 1;
        numDistance.Increment = new decimal(new int[] { 5, 0, 0, 65536 }); // шаг 0,5
        numDistance.Location = new Point(140, 92);
        numDistance.Maximum = new decimal(new int[] { 300, 0, 0, 0 });
        numDistance.Name = "numDistance";
        numDistance.Size = new Size(80, 23);
        numDistance.TabIndex = 2;
        numDistance.Value = new decimal(new int[] { 5, 0, 0, 0 });
        numDistance.ValueChanged += numDistance_ValueChanged;

        // lblKm
        lblKm.AutoSize = true;
        lblKm.Location = new Point(226, 95);
        lblKm.Name = "lblKm";
        lblKm.Text = "км";

        // ---- gbTariff: группа «Тариф» ----
        gbTariff.Controls.Add(cmbTariff);
        gbTariff.Controls.Add(lblTariffInfo);
        gbTariff.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        gbTariff.Location = new Point(12, 147);
        gbTariff.Name = "gbTariff";
        gbTariff.Size = new Size(600, 75);
        gbTariff.TabIndex = 1;
        gbTariff.TabStop = false;
        gbTariff.Text = "Тариф";

        // cmbTariff
        cmbTariff.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbTariff.FormattingEnabled = true;
        cmbTariff.Location = new Point(15, 28);
        cmbTariff.Name = "cmbTariff";
        cmbTariff.Size = new Size(180, 23);
        cmbTariff.TabIndex = 0;
        cmbTariff.SelectedIndexChanged += cmbTariff_SelectedIndexChanged;

        // lblTariffInfo
        lblTariffInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblTariffInfo.ForeColor = Color.DimGray;
        lblTariffInfo.Location = new Point(210, 22);
        lblTariffInfo.Name = "lblTariffInfo";
        lblTariffInfo.Size = new Size(370, 42);
        lblTariffInfo.Text = "";

        // ---- gbOptions: группа «Дополнительные опции» ----
        gbOptions.Controls.Add(chkChildSeat);
        gbOptions.Controls.Add(chkPets);
        gbOptions.Controls.Add(chkLuggage);
        gbOptions.Anchor = AnchorStyles.Top | AnchorStyles.Left;
        gbOptions.Location = new Point(12, 232);
        gbOptions.Name = "gbOptions";
        gbOptions.Size = new Size(300, 120);
        gbOptions.TabIndex = 2;
        gbOptions.TabStop = false;
        gbOptions.Text = "Дополнительные опции";

        // chkChildSeat
        chkChildSeat.AutoSize = true;
        chkChildSeat.Location = new Point(15, 28);
        chkChildSeat.Name = "chkChildSeat";
        chkChildSeat.TabIndex = 0;
        chkChildSeat.Text = "Детское кресло (+50 ₽)";
        chkChildSeat.UseVisualStyleBackColor = true;
        chkChildSeat.CheckedChanged += chkOption_CheckedChanged;

        // chkPets
        chkPets.AutoSize = true;
        chkPets.Location = new Point(15, 58);
        chkPets.Name = "chkPets";
        chkPets.TabIndex = 1;
        chkPets.Text = "Перевозка животных (+100 ₽)";
        chkPets.UseVisualStyleBackColor = true;
        chkPets.CheckedChanged += chkOption_CheckedChanged;

        // chkLuggage
        chkLuggage.AutoSize = true;
        chkLuggage.Location = new Point(15, 88);
        chkLuggage.Name = "chkLuggage";
        chkLuggage.TabIndex = 2;
        chkLuggage.Text = "Багаж (+70 ₽)";
        chkLuggage.UseVisualStyleBackColor = true;
        chkLuggage.CheckedChanged += chkOption_CheckedChanged;

        // ---- gbPayment: группа «Форма оплаты» ----
        gbPayment.Controls.Add(rbCash);
        gbPayment.Controls.Add(rbCard);
        gbPayment.Controls.Add(lblPaymentHint);
        gbPayment.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        gbPayment.Location = new Point(324, 232);
        gbPayment.Name = "gbPayment";
        gbPayment.Size = new Size(288, 120);
        gbPayment.TabIndex = 3;
        gbPayment.TabStop = false;
        gbPayment.Text = "Форма оплаты";

        // rbCash
        rbCash.AutoSize = true;
        rbCash.Checked = true;
        rbCash.Location = new Point(15, 28);
        rbCash.Name = "rbCash";
        rbCash.TabIndex = 0;
        rbCash.TabStop = true;
        rbCash.Text = "Наличные";
        rbCash.UseVisualStyleBackColor = true;
        rbCash.CheckedChanged += rbPayment_CheckedChanged;

        // rbCard
        rbCard.AutoSize = true;
        rbCard.Location = new Point(15, 56);
        rbCard.Name = "rbCard";
        rbCard.TabIndex = 1;
        rbCard.Text = "Карта";
        rbCard.UseVisualStyleBackColor = true;
        rbCard.CheckedChanged += rbPayment_CheckedChanged;

        // lblPaymentHint
        lblPaymentHint.Location = new Point(15, 86);
        lblPaymentHint.Name = "lblPaymentHint";
        lblPaymentHint.Size = new Size(260, 22);
        lblPaymentHint.Text = "";

        // ---- lblTotal: итоговая стоимость ----
        lblTotal.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblTotal.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblTotal.Location = new Point(12, 362);
        lblTotal.Name = "lblTotal";
        lblTotal.Size = new Size(600, 28);
        lblTotal.Text = "";
        lblTotal.TextAlign = ContentAlignment.MiddleLeft;

        // ---- кнопки ----
        btnOrder.Location = new Point(12, 398);
        btnOrder.Name = "btnOrder";
        btnOrder.Size = new Size(150, 36);
        btnOrder.TabIndex = 4;
        btnOrder.Text = "Вызвать такси";
        btnOrder.UseVisualStyleBackColor = true;
        btnOrder.Click += btnOrder_Click;

        btnClear.Location = new Point(172, 398);
        btnClear.Name = "btnClear";
        btnClear.Size = new Size(120, 36);
        btnClear.TabIndex = 5;
        btnClear.Text = "Очистить";
        btnClear.UseVisualStyleBackColor = true;
        btnClear.Click += btnClear_Click;

        btnDelete.Enabled = false;
        btnDelete.Location = new Point(302, 398);
        btnDelete.Name = "btnDelete";
        btnDelete.Size = new Size(170, 36);
        btnDelete.TabIndex = 6;
        btnDelete.Text = "Удалить выбранный";
        btnDelete.UseVisualStyleBackColor = true;
        btnDelete.Click += btnDelete_Click;

        // ---- lblStatus: уведомления и ошибки ----
        lblStatus.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblStatus.BorderStyle = BorderStyle.FixedSingle;
        lblStatus.Location = new Point(12, 442);
        lblStatus.Name = "lblStatus";
        lblStatus.Padding = new Padding(6, 0, 6, 0);
        lblStatus.Size = new Size(600, 40);
        lblStatus.Text = "";
        lblStatus.TextAlign = ContentAlignment.MiddleLeft;

        // ---- gbOrders: список заказов ----
        gbOrders.Controls.Add(lstOrders);
        gbOrders.Controls.Add(lblDetails);
        gbOrders.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        gbOrders.Location = new Point(12, 490);
        gbOrders.Name = "gbOrders";
        gbOrders.Size = new Size(600, 200);
        gbOrders.TabIndex = 7;
        gbOrders.TabStop = false;
        gbOrders.Text = "Оформленные заказы";

        // lstOrders
        lstOrders.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        lstOrders.ContextMenuStrip = ctxOrders;
        lstOrders.FormattingEnabled = true;
        lstOrders.HorizontalScrollbar = true;
        lstOrders.IntegralHeight = false;
        lstOrders.ItemHeight = 15;
        lstOrders.Location = new Point(10, 22);
        lstOrders.Name = "lstOrders";
        lstOrders.Size = new Size(580, 112);
        lstOrders.TabIndex = 0;
        lstOrders.SelectedIndexChanged += lstOrders_SelectedIndexChanged;
        lstOrders.KeyDown += lstOrders_KeyDown;
        lstOrders.MouseDown += lstOrders_MouseDown;

        // lblDetails
        lblDetails.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        lblDetails.ForeColor = Color.DimGray;
        lblDetails.Location = new Point(10, 140);
        lblDetails.Name = "lblDetails";
        lblDetails.Size = new Size(580, 52);
        lblDetails.Text = "";

        // ---- контекстное меню списка ----
        ctxOrders.Items.AddRange(new ToolStripItem[] { miDeleteOrder });
        ctxOrders.Name = "ctxOrders";

        miDeleteOrder.Name = "miDeleteOrder";
        miDeleteOrder.Text = "Удалить заказ";
        miDeleteOrder.Click += btnDelete_Click;

        // ---- сама форма ----
        AcceptButton = btnOrder;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(624, 705);
        Controls.Add(gbRoute);
        Controls.Add(gbTariff);
        Controls.Add(gbOptions);
        Controls.Add(gbPayment);
        Controls.Add(lblTotal);
        Controls.Add(btnOrder);
        Controls.Add(btnClear);
        Controls.Add(btnDelete);
        Controls.Add(lblStatus);
        Controls.Add(gbOrders);
        Font = new Font("Segoe UI", 9F);
        MinimumSize = new Size(640, 700);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Заказ услуг такси";

        gbRoute.ResumeLayout(false);
        gbRoute.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)numDistance).EndInit();
        gbTariff.ResumeLayout(false);
        gbOptions.ResumeLayout(false);
        gbOptions.PerformLayout();
        gbPayment.ResumeLayout(false);
        gbPayment.PerformLayout();
        gbOrders.ResumeLayout(false);
        ctxOrders.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private GroupBox gbRoute;
    private Label lblFrom;
    private TextBox txtFrom;
    private Label lblTo;
    private TextBox txtTo;
    private Label lblDistance;
    private NumericUpDown numDistance;
    private Label lblKm;

    private GroupBox gbTariff;
    private ComboBox cmbTariff;
    private Label lblTariffInfo;

    private GroupBox gbOptions;
    private CheckBox chkChildSeat;
    private CheckBox chkPets;
    private CheckBox chkLuggage;

    private GroupBox gbPayment;
    private RadioButton rbCash;
    private RadioButton rbCard;
    private Label lblPaymentHint;

    private Label lblTotal;
    private Button btnOrder;
    private Button btnClear;
    private Button btnDelete;
    private Label lblStatus;

    private GroupBox gbOrders;
    private ListBox lstOrders;
    private Label lblDetails;

    private ContextMenuStrip ctxOrders;
    private ToolStripMenuItem miDeleteOrder;
    private ToolTip toolTip;
}
