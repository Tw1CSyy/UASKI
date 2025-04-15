using System.Windows.Forms;

using UASKI.Core;
using UASKI.Models;
using UASKI.StaticModels;

namespace UASKI
{
    partial class Gl_Form
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            DataConnection.Close();
           
            if (disposing && (components != null))
            {
                components.Dispose();
               
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Gl_Form));
            this.Menu_Step1 = new System.Windows.Forms.ListBox();
            this.Menu_Step2 = new System.Windows.Forms.ListBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button19 = new System.Windows.Forms.Button();
            this.panel12 = new System.Windows.Forms.Panel();
            this.button18 = new System.Windows.Forms.Button();
            this.label58 = new System.Windows.Forms.Label();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.ispDataGridView = new System.Windows.Forms.DataGridView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.button20 = new System.Windows.Forms.Button();
            this.panel13 = new System.Windows.Forms.Panel();
            this.label62 = new System.Windows.Forms.Label();
            this.button24 = new System.Windows.Forms.Button();
            this.button21 = new System.Windows.Forms.Button();
            this.panel16 = new System.Windows.Forms.Panel();
            this.button29 = new System.Windows.Forms.Button();
            this.button28 = new System.Windows.Forms.Button();
            this.dateTimePicker5 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker6 = new System.Windows.Forms.DateTimePicker();
            this.label60 = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.textBox29 = new System.Windows.Forms.TextBox();
            this.label59 = new System.Windows.Forms.Label();
            this.textBox19 = new System.Windows.Forms.TextBox();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.button16 = new System.Windows.Forms.Button();
            this.panel14 = new System.Windows.Forms.Panel();
            this.button25 = new System.Windows.Forms.Button();
            this.button17 = new System.Windows.Forms.Button();
            this.panel15 = new System.Windows.Forms.Panel();
            this.button31 = new System.Windows.Forms.Button();
            this.button30 = new System.Windows.Forms.Button();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker3 = new System.Windows.Forms.DateTimePicker();
            this.label65 = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label39 = new System.Windows.Forms.Label();
            this.textBox31 = new System.Windows.Forms.TextBox();
            this.label66 = new System.Windows.Forms.Label();
            this.textBox32 = new System.Windows.Forms.TextBox();
            this.dataGridView5 = new System.Windows.Forms.DataGridView();
            this.tabPage10 = new System.Windows.Forms.TabPage();
            this.monthCalendar2 = new System.Windows.Forms.MonthCalendar();
            this.button13 = new System.Windows.Forms.Button();
            this.dataGridView6 = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.button22 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel17 = new System.Windows.Forms.Panel();
            this.label63 = new System.Windows.Forms.Label();
            this.button26 = new System.Windows.Forms.Button();
            this.button23 = new System.Windows.Forms.Button();
            this.panel18 = new System.Windows.Forms.Panel();
            this.button33 = new System.Windows.Forms.Button();
            this.button32 = new System.Windows.Forms.Button();
            this.dateTimePicker7 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker8 = new System.Windows.Forms.DateTimePicker();
            this.label67 = new System.Windows.Forms.Label();
            this.label68 = new System.Windows.Forms.Label();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.textBox33 = new System.Windows.Forms.TextBox();
            this.textBox34 = new System.Windows.Forms.TextBox();
            this.label71 = new System.Windows.Forms.Label();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.checkBox8 = new System.Windows.Forms.CheckBox();
            this.button14 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label26 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label25 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.panel6 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.label27 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.button5 = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.IspCon = new System.Windows.Forms.TabPage();
            this.dataGridView4 = new System.Windows.Forms.DataGridView();
            this.panel8 = new System.Windows.Forms.Panel();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.textBox15 = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.textBox16 = new System.Windows.Forms.TextBox();
            this.label36 = new System.Windows.Forms.Label();
            this.textBox17 = new System.Windows.Forms.TextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.textBox18 = new System.Windows.Forms.TextBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.Tasks = new System.Windows.Forms.TabPage();
            this.panel19 = new System.Windows.Forms.Panel();
            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.label57 = new System.Windows.Forms.Label();
            this.textBox28 = new System.Windows.Forms.TextBox();
            this.label52 = new System.Windows.Forms.Label();
            this.button27 = new System.Windows.Forms.Button();
            this.dateTimePicker9 = new System.Windows.Forms.DateTimePicker();
            this.label38 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.button48 = new System.Windows.Forms.Button();
            this.button47 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.panel9 = new System.Windows.Forms.Panel();
            this.button15 = new System.Windows.Forms.Button();
            this.label72 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.dateTimePicker4 = new System.Windows.Forms.DateTimePicker();
            this.label41 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.textBox27 = new System.Windows.Forms.TextBox();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.label43 = new System.Windows.Forms.Label();
            this.textBox21 = new System.Windows.Forms.TextBox();
            this.label44 = new System.Windows.Forms.Label();
            this.textBox22 = new System.Windows.Forms.TextBox();
            this.label45 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.textBox23 = new System.Windows.Forms.TextBox();
            this.label47 = new System.Windows.Forms.Label();
            this.textBox24 = new System.Windows.Forms.TextBox();
            this.label48 = new System.Windows.Forms.Label();
            this.textBox25 = new System.Windows.Forms.TextBox();
            this.label49 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.textBox26 = new System.Windows.Forms.TextBox();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.panel23 = new System.Windows.Forms.Panel();
            this.textBox30 = new System.Windows.Forms.TextBox();
            this.button36 = new System.Windows.Forms.Button();
            this.button34 = new System.Windows.Forms.Button();
            this.textBox20 = new System.Windows.Forms.TextBox();
            this.label76 = new System.Windows.Forms.Label();
            this.label75 = new System.Windows.Forms.Label();
            this.dateTimePicker11 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker10 = new System.Windows.Forms.DateTimePicker();
            this.label74 = new System.Windows.Forms.Label();
            this.dataGridView7 = new System.Windows.Forms.DataGridView();
            this.tabPage11 = new System.Windows.Forms.TabPage();
            this.dataGridView8 = new System.Windows.Forms.DataGridView();
            this.panel24 = new System.Windows.Forms.Panel();
            this.dateTimePicker19 = new System.Windows.Forms.DateTimePicker();
            this.label78 = new System.Windows.Forms.Label();
            this.button37 = new System.Windows.Forms.Button();
            this.label77 = new System.Windows.Forms.Label();
            this.tabPage12 = new System.Windows.Forms.TabPage();
            this.dataGridView9 = new System.Windows.Forms.DataGridView();
            this.panel25 = new System.Windows.Forms.Panel();
            this.button38 = new System.Windows.Forms.Button();
            this.textBox35 = new System.Windows.Forms.TextBox();
            this.label79 = new System.Windows.Forms.Label();
            this.label80 = new System.Windows.Forms.Label();
            this.tabPage13 = new System.Windows.Forms.TabPage();
            this.dataGridView10 = new System.Windows.Forms.DataGridView();
            this.panel26 = new System.Windows.Forms.Panel();
            this.button40 = new System.Windows.Forms.Button();
            this.label83 = new System.Windows.Forms.Label();
            this.label81 = new System.Windows.Forms.Label();
            this.dateTimePicker12 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker13 = new System.Windows.Forms.DateTimePicker();
            this.label82 = new System.Windows.Forms.Label();
            this.tabPage14 = new System.Windows.Forms.TabPage();
            this.dataGridView13 = new System.Windows.Forms.DataGridView();
            this.dataGridView11 = new System.Windows.Forms.DataGridView();
            this.panel27 = new System.Windows.Forms.Panel();
            this.textBox36 = new System.Windows.Forms.TextBox();
            this.button43 = new System.Windows.Forms.Button();
            this.textBox37 = new System.Windows.Forms.TextBox();
            this.label87 = new System.Windows.Forms.Label();
            this.label86 = new System.Windows.Forms.Label();
            this.label84 = new System.Windows.Forms.Label();
            this.button42 = new System.Windows.Forms.Button();
            this.label85 = new System.Windows.Forms.Label();
            this.dateTimePicker14 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker15 = new System.Windows.Forms.DateTimePicker();
            this.panel28 = new System.Windows.Forms.Panel();
            this.label100 = new System.Windows.Forms.Label();
            this.label99 = new System.Windows.Forms.Label();
            this.tabPage15 = new System.Windows.Forms.TabPage();
            this.panel20 = new System.Windows.Forms.Panel();
            this.button44 = new System.Windows.Forms.Button();
            this.button45 = new System.Windows.Forms.Button();
            this.button46 = new System.Windows.Forms.Button();
            this.panel10 = new System.Windows.Forms.Panel();
            this.label94 = new System.Windows.Forms.Label();
            this.label93 = new System.Windows.Forms.Label();
            this.label92 = new System.Windows.Forms.Label();
            this.label91 = new System.Windows.Forms.Label();
            this.textBox39 = new System.Windows.Forms.TextBox();
            this.dateTimePicker16 = new System.Windows.Forms.DateTimePicker();
            this.label90 = new System.Windows.Forms.Label();
            this.label89 = new System.Windows.Forms.Label();
            this.textBox38 = new System.Windows.Forms.TextBox();
            this.label102 = new System.Windows.Forms.Label();
            this.tabPage16 = new System.Windows.Forms.TabPage();
            this.button54 = new System.Windows.Forms.Button();
            this.dataGridView12 = new System.Windows.Forms.DataGridView();
            this.panel21 = new System.Windows.Forms.Panel();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.button51 = new System.Windows.Forms.Button();
            this.panel22 = new System.Windows.Forms.Panel();
            this.button52 = new System.Windows.Forms.Button();
            this.button53 = new System.Windows.Forms.Button();
            this.dateTimePicker17 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker18 = new System.Windows.Forms.DateTimePicker();
            this.label95 = new System.Windows.Forms.Label();
            this.label96 = new System.Windows.Forms.Label();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.textBox40 = new System.Windows.Forms.TextBox();
            this.label98 = new System.Windows.Forms.Label();
            this.tabPage17 = new System.Windows.Forms.TabPage();
            this.panel29 = new System.Windows.Forms.Panel();
            this.textBox42 = new System.Windows.Forms.TextBox();
            this.button50 = new System.Windows.Forms.Button();
            this.button56 = new System.Windows.Forms.Button();
            this.textBox43 = new System.Windows.Forms.TextBox();
            this.label54 = new System.Windows.Forms.Label();
            this.label69 = new System.Windows.Forms.Label();
            this.dateTimePicker20 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker21 = new System.Windows.Forms.DateTimePicker();
            this.label70 = new System.Windows.Forms.Label();
            this.dataGridView14 = new System.Windows.Forms.DataGridView();
            this.tabPage18 = new System.Windows.Forms.TabPage();
            this.button57 = new System.Windows.Forms.Button();
            this.dataGridView15 = new System.Windows.Forms.DataGridView();
            this.panel30 = new System.Windows.Forms.Panel();
            this.button59 = new System.Windows.Forms.Button();
            this.panel31 = new System.Windows.Forms.Panel();
            this.button60 = new System.Windows.Forms.Button();
            this.button61 = new System.Windows.Forms.Button();
            this.dateTimePicker22 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker23 = new System.Windows.Forms.DateTimePicker();
            this.label88 = new System.Windows.Forms.Label();
            this.label97 = new System.Windows.Forms.Label();
            this.checkBox9 = new System.Windows.Forms.CheckBox();
            this.textBox44 = new System.Windows.Forms.TextBox();
            this.label101 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.textBox41 = new System.Windows.Forms.TextBox();
            this.TimeTimer = new System.Windows.Forms.Timer(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.panel12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ispDataGridView)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.panel13.SuspendLayout();
            this.panel16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.tabPage9.SuspendLayout();
            this.panel14.SuspendLayout();
            this.panel15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView5)).BeginInit();
            this.tabPage10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView6)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel17.SuspendLayout();
            this.panel18.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.tabPage7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.IspCon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).BeginInit();
            this.panel8.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.Tasks.SuspendLayout();
            this.panel19.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel9.SuspendLayout();
            this.tabPage8.SuspendLayout();
            this.panel23.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView7)).BeginInit();
            this.tabPage11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView8)).BeginInit();
            this.panel24.SuspendLayout();
            this.tabPage12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView9)).BeginInit();
            this.panel25.SuspendLayout();
            this.tabPage13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView10)).BeginInit();
            this.panel26.SuspendLayout();
            this.tabPage14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView11)).BeginInit();
            this.panel27.SuspendLayout();
            this.panel28.SuspendLayout();
            this.tabPage15.SuspendLayout();
            this.panel20.SuspendLayout();
            this.panel10.SuspendLayout();
            this.tabPage16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView12)).BeginInit();
            this.panel21.SuspendLayout();
            this.panel22.SuspendLayout();
            this.tabPage17.SuspendLayout();
            this.panel29.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView14)).BeginInit();
            this.tabPage18.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView15)).BeginInit();
            this.panel30.SuspendLayout();
            this.panel31.SuspendLayout();
            this.SuspendLayout();
            // 
            // Menu_Step1
            // 
            this.Menu_Step1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Menu_Step1.CausesValidation = false;
            this.Menu_Step1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Menu_Step1.ForeColor = System.Drawing.Color.Black;
            this.Menu_Step1.ItemHeight = 24;
            this.Menu_Step1.Location = new System.Drawing.Point(-1, 1);
            this.Menu_Step1.Name = "Menu_Step1";
            this.Menu_Step1.Size = new System.Drawing.Size(439, 148);
            this.Menu_Step1.TabIndex = 0;
            this.Menu_Step1.Visible = false;
            this.Menu_Step1.SelectedIndexChanged += new System.EventHandler(this.Menu_Step1_SelectedIndexChanged);
            this.Menu_Step1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Menu_Step1_KeyDown);
            // 
            // Menu_Step2
            // 
            this.Menu_Step2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Menu_Step2.Enabled = false;
            this.Menu_Step2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Menu_Step2.ForeColor = System.Drawing.Color.Black;
            this.Menu_Step2.FormattingEnabled = true;
            this.Menu_Step2.ItemHeight = 24;
            this.Menu_Step2.Location = new System.Drawing.Point(444, 1);
            this.Menu_Step2.Name = "Menu_Step2";
            this.Menu_Step2.Size = new System.Drawing.Size(444, 148);
            this.Menu_Step2.TabIndex = 1;
            this.Menu_Step2.Visible = false;
            this.Menu_Step2.Click += new System.EventHandler(this.Menu_Step2_Click);
            this.Menu_Step2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Menu_Step2_KeyDown);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage9);
            this.tabControl1.Controls.Add(this.tabPage10);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Controls.Add(this.IspCon);
            this.tabControl1.Controls.Add(this.Tasks);
            this.tabControl1.Controls.Add(this.tabPage8);
            this.tabControl1.Controls.Add(this.tabPage11);
            this.tabControl1.Controls.Add(this.tabPage12);
            this.tabControl1.Controls.Add(this.tabPage13);
            this.tabControl1.Controls.Add(this.tabPage14);
            this.tabControl1.Controls.Add(this.tabPage15);
            this.tabControl1.Controls.Add(this.tabPage16);
            this.tabControl1.Controls.Add(this.tabPage17);
            this.tabControl1.Controls.Add(this.tabPage18);
            this.tabControl1.Location = new System.Drawing.Point(2, 153);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(889, 569);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(881, 543);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Главная";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(28, 58);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(826, 412);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tabPage2.Controls.Add(this.button19);
            this.tabPage2.Controls.Add(this.panel12);
            this.tabPage2.Controls.Add(this.ispDataGridView);
            this.tabPage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(881, 543);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Просмотр Исп-Кон";
            // 
            // button19
            // 
            this.button19.BackColor = System.Drawing.Color.White;
            this.button19.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button19.Location = new System.Drawing.Point(2, 0);
            this.button19.Name = "button19";
            this.button19.Size = new System.Drawing.Size(17, 30);
            this.button19.TabIndex = 26;
            this.button19.Text = "<";
            this.button19.UseVisualStyleBackColor = false;
            this.button19.Click += new System.EventHandler(this.button19_Click);
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel12.Controls.Add(this.button18);
            this.panel12.Controls.Add(this.label58);
            this.panel12.Controls.Add(this.textBox13);
            this.panel12.Location = new System.Drawing.Point(0, 0);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(248, 547);
            this.panel12.TabIndex = 1;
            this.panel12.Visible = false;
            // 
            // button18
            // 
            this.button18.BackColor = System.Drawing.Color.White;
            this.button18.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button18.Location = new System.Drawing.Point(228, 0);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(17, 30);
            this.button18.TabIndex = 28;
            this.button18.Text = ">";
            this.button18.UseVisualStyleBackColor = false;
            this.button18.Click += new System.EventHandler(this.button18_Click);
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(1, 13);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(55, 20);
            this.label58.TabIndex = 1;
            this.label58.Text = "Поиск";
            // 
            // textBox13
            // 
            this.textBox13.Location = new System.Drawing.Point(3, 36);
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new System.Drawing.Size(235, 26);
            this.textBox13.TabIndex = 0;
            this.textBox13.TextChanged += new System.EventHandler(this.textBox13_TextChanged);
            this.textBox13.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox13_KeyDown);
            // 
            // ispDataGridView
            // 
            this.ispDataGridView.AllowUserToAddRows = false;
            this.ispDataGridView.AllowUserToDeleteRows = false;
            this.ispDataGridView.BackgroundColor = System.Drawing.Color.Silver;
            this.ispDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ispDataGridView.Location = new System.Drawing.Point(246, 0);
            this.ispDataGridView.MultiSelect = false;
            this.ispDataGridView.Name = "ispDataGridView";
            this.ispDataGridView.ReadOnly = true;
            this.ispDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ispDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ispDataGridView.Size = new System.Drawing.Size(636, 547);
            this.ispDataGridView.TabIndex = 0;
            this.ispDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.IspDataGridView_CellDoubleClick);
            this.ispDataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.IspDataGridView_KeyDown);
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tabPage4.Controls.Add(this.button20);
            this.tabPage4.Controls.Add(this.panel13);
            this.tabPage4.Controls.Add(this.dataGridView3);
            this.tabPage4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(881, 543);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Просмотр задач";
            // 
            // button20
            // 
            this.button20.BackColor = System.Drawing.Color.White;
            this.button20.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button20.Location = new System.Drawing.Point(2, 0);
            this.button20.Name = "button20";
            this.button20.Size = new System.Drawing.Size(17, 30);
            this.button20.TabIndex = 27;
            this.button20.Text = "<";
            this.button20.UseVisualStyleBackColor = false;
            this.button20.Click += new System.EventHandler(this.button20_Click);
            // 
            // panel13
            // 
            this.panel13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel13.Controls.Add(this.label62);
            this.panel13.Controls.Add(this.button24);
            this.panel13.Controls.Add(this.button21);
            this.panel13.Controls.Add(this.panel16);
            this.panel13.Controls.Add(this.checkBox2);
            this.panel13.Controls.Add(this.textBox29);
            this.panel13.Controls.Add(this.label59);
            this.panel13.Controls.Add(this.textBox19);
            this.panel13.Location = new System.Drawing.Point(0, 0);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(246, 542);
            this.panel13.TabIndex = 2;
            this.panel13.Visible = false;
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label62.Location = new System.Drawing.Point(3, 80);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(140, 17);
            this.label62.TabIndex = 30;
            this.label62.Text = "Код подразделения";
            // 
            // button24
            // 
            this.button24.BackColor = System.Drawing.Color.White;
            this.button24.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button24.Location = new System.Drawing.Point(224, 100);
            this.button24.Name = "button24";
            this.button24.Size = new System.Drawing.Size(19, 26);
            this.button24.TabIndex = 29;
            this.button24.Text = "^";
            this.button24.UseVisualStyleBackColor = false;
            this.button24.Click += new System.EventHandler(this.button24_Click);
            // 
            // button21
            // 
            this.button21.BackColor = System.Drawing.Color.White;
            this.button21.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button21.Location = new System.Drawing.Point(223, 0);
            this.button21.Name = "button21";
            this.button21.Size = new System.Drawing.Size(17, 30);
            this.button21.TabIndex = 28;
            this.button21.Text = ">";
            this.button21.UseVisualStyleBackColor = false;
            this.button21.Click += new System.EventHandler(this.button21_Click);
            // 
            // panel16
            // 
            this.panel16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel16.Controls.Add(this.button29);
            this.panel16.Controls.Add(this.button28);
            this.panel16.Controls.Add(this.dateTimePicker5);
            this.panel16.Controls.Add(this.dateTimePicker6);
            this.panel16.Controls.Add(this.label60);
            this.panel16.Controls.Add(this.label61);
            this.panel16.Location = new System.Drawing.Point(4, 179);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(241, 129);
            this.panel16.TabIndex = 15;
            // 
            // button29
            // 
            this.button29.BackColor = System.Drawing.Color.White;
            this.button29.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button29.Location = new System.Drawing.Point(220, 79);
            this.button29.Name = "button29";
            this.button29.Size = new System.Drawing.Size(19, 26);
            this.button29.TabIndex = 31;
            this.button29.Text = "^";
            this.button29.UseVisualStyleBackColor = false;
            this.button29.Click += new System.EventHandler(this.button29_Click);
            // 
            // button28
            // 
            this.button28.BackColor = System.Drawing.Color.White;
            this.button28.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button28.Location = new System.Drawing.Point(220, 27);
            this.button28.Name = "button28";
            this.button28.Size = new System.Drawing.Size(19, 26);
            this.button28.TabIndex = 30;
            this.button28.Text = "^";
            this.button28.UseVisualStyleBackColor = false;
            this.button28.Click += new System.EventHandler(this.button28_Click);
            // 
            // dateTimePicker5
            // 
            this.dateTimePicker5.Location = new System.Drawing.Point(5, 27);
            this.dateTimePicker5.Name = "dateTimePicker5";
            this.dateTimePicker5.Size = new System.Drawing.Size(212, 26);
            this.dateTimePicker5.TabIndex = 5;
            this.dateTimePicker5.ValueChanged += new System.EventHandler(this.dateTimePicker5_ValueChanged);
            this.dateTimePicker5.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dateTimePicker5_KeyDown);
            // 
            // dateTimePicker6
            // 
            this.dateTimePicker6.Location = new System.Drawing.Point(5, 79);
            this.dateTimePicker6.Name = "dateTimePicker6";
            this.dateTimePicker6.Size = new System.Drawing.Size(212, 26);
            this.dateTimePicker6.TabIndex = 4;
            this.dateTimePicker6.ValueChanged += new System.EventHandler(this.dateTimePicker6_ValueChanged);
            this.dateTimePicker6.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dateTimePicker6_KeyDown);
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label60.Location = new System.Drawing.Point(1, 4);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(30, 20);
            this.label60.TabIndex = 6;
            this.label60.Text = "От";
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label61.Location = new System.Drawing.Point(1, 56);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(30, 20);
            this.label61.TabIndex = 7;
            this.label61.Text = "До";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(7, 149);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(199, 24);
            this.checkBox2.TabIndex = 14;
            this.checkBox2.Text = "Учитывать дату срока";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            this.checkBox2.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.checkBox2_PreviewKeyDown);
            // 
            // textBox29
            // 
            this.textBox29.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox29.Location = new System.Drawing.Point(7, 100);
            this.textBox29.Name = "textBox29";
            this.textBox29.Size = new System.Drawing.Size(214, 26);
            this.textBox29.TabIndex = 8;
            this.textBox29.TextChanged += new System.EventHandler(this.textBox29_TextChanged);
            this.textBox29.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox29_KeyDown);
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label59.Location = new System.Drawing.Point(3, 14);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(39, 20);
            this.label59.TabIndex = 3;
            this.label59.Text = "Код";
            // 
            // textBox19
            // 
            this.textBox19.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox19.Location = new System.Drawing.Point(7, 37);
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new System.Drawing.Size(235, 26);
            this.textBox19.TabIndex = 2;
            this.textBox19.TextChanged += new System.EventHandler(this.textBox19_TextChanged_1);
            this.textBox19.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox19_KeyDown);
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.AllowUserToDeleteRows = false;
            this.dataGridView3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(246, 0);
            this.dataGridView3.MultiSelect = false;
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.ReadOnly = true;
            this.dataGridView3.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView3.Size = new System.Drawing.Size(636, 547);
            this.dataGridView3.TabIndex = 0;
            this.dataGridView3.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView3_CellDoubleClick);
            this.dataGridView3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView3_KeyDown);
            // 
            // tabPage9
            // 
            this.tabPage9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tabPage9.Controls.Add(this.button16);
            this.tabPage9.Controls.Add(this.panel14);
            this.tabPage9.Controls.Add(this.dataGridView5);
            this.tabPage9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.tabPage9.Location = new System.Drawing.Point(4, 22);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Size = new System.Drawing.Size(881, 543);
            this.tabPage9.TabIndex = 8;
            this.tabPage9.Text = "Просмотр Архива";
            // 
            // button16
            // 
            this.button16.BackColor = System.Drawing.Color.White;
            this.button16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button16.Location = new System.Drawing.Point(2, 0);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(17, 30);
            this.button16.TabIndex = 25;
            this.button16.Text = "<";
            this.button16.UseVisualStyleBackColor = false;
            this.button16.Click += new System.EventHandler(this.button16_Click);
            // 
            // panel14
            // 
            this.panel14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel14.Controls.Add(this.button25);
            this.panel14.Controls.Add(this.button17);
            this.panel14.Controls.Add(this.panel15);
            this.panel14.Controls.Add(this.checkBox1);
            this.panel14.Controls.Add(this.label39);
            this.panel14.Controls.Add(this.textBox31);
            this.panel14.Controls.Add(this.label66);
            this.panel14.Controls.Add(this.textBox32);
            this.panel14.Location = new System.Drawing.Point(0, 0);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(246, 542);
            this.panel14.TabIndex = 4;
            this.panel14.Visible = false;
            // 
            // button25
            // 
            this.button25.BackColor = System.Drawing.Color.White;
            this.button25.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button25.Location = new System.Drawing.Point(221, 102);
            this.button25.Name = "button25";
            this.button25.Size = new System.Drawing.Size(19, 26);
            this.button25.TabIndex = 27;
            this.button25.Text = "^";
            this.button25.UseVisualStyleBackColor = false;
            this.button25.Click += new System.EventHandler(this.button25_Click);
            // 
            // button17
            // 
            this.button17.BackColor = System.Drawing.Color.White;
            this.button17.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button17.Location = new System.Drawing.Point(226, 0);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(17, 30);
            this.button17.TabIndex = 26;
            this.button17.Text = ">";
            this.button17.UseVisualStyleBackColor = false;
            this.button17.Click += new System.EventHandler(this.button17_Click);
            // 
            // panel15
            // 
            this.panel15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel15.Controls.Add(this.button31);
            this.panel15.Controls.Add(this.button30);
            this.panel15.Controls.Add(this.dateTimePicker2);
            this.panel15.Controls.Add(this.dateTimePicker3);
            this.panel15.Controls.Add(this.label65);
            this.panel15.Controls.Add(this.label64);
            this.panel15.Location = new System.Drawing.Point(2, 188);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(241, 129);
            this.panel15.TabIndex = 13;
            // 
            // button31
            // 
            this.button31.BackColor = System.Drawing.Color.White;
            this.button31.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button31.Location = new System.Drawing.Point(219, 79);
            this.button31.Name = "button31";
            this.button31.Size = new System.Drawing.Size(19, 26);
            this.button31.TabIndex = 31;
            this.button31.Text = "^";
            this.button31.UseVisualStyleBackColor = false;
            this.button31.Click += new System.EventHandler(this.button31_Click);
            // 
            // button30
            // 
            this.button30.BackColor = System.Drawing.Color.White;
            this.button30.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button30.Location = new System.Drawing.Point(219, 27);
            this.button30.Name = "button30";
            this.button30.Size = new System.Drawing.Size(19, 26);
            this.button30.TabIndex = 30;
            this.button30.Text = "^";
            this.button30.UseVisualStyleBackColor = false;
            this.button30.Click += new System.EventHandler(this.button30_Click);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(5, 27);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(212, 26);
            this.dateTimePicker2.TabIndex = 5;
            this.dateTimePicker2.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
            this.dateTimePicker2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dateTimePicker2_KeyDown);
            // 
            // dateTimePicker3
            // 
            this.dateTimePicker3.Location = new System.Drawing.Point(5, 79);
            this.dateTimePicker3.Name = "dateTimePicker3";
            this.dateTimePicker3.Size = new System.Drawing.Size(212, 26);
            this.dateTimePicker3.TabIndex = 4;
            this.dateTimePicker3.ValueChanged += new System.EventHandler(this.dateTimePicker3_ValueChanged);
            this.dateTimePicker3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dateTimePicker3_KeyDown);
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label65.Location = new System.Drawing.Point(1, 4);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(30, 20);
            this.label65.TabIndex = 6;
            this.label65.Text = "От";
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label64.Location = new System.Drawing.Point(1, 56);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(30, 20);
            this.label64.TabIndex = 7;
            this.label64.Text = "До";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(5, 158);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(228, 24);
            this.checkBox1.TabIndex = 12;
            this.checkBox1.Text = "Учитывать дату закрытия";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            this.checkBox1.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.checkBox1_PreviewKeyDown);
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label39.Location = new System.Drawing.Point(1, 79);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(140, 17);
            this.label39.TabIndex = 11;
            this.label39.Text = "Код подразделения";
            // 
            // textBox31
            // 
            this.textBox31.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox31.Location = new System.Drawing.Point(5, 102);
            this.textBox31.Name = "textBox31";
            this.textBox31.Size = new System.Drawing.Size(214, 26);
            this.textBox31.TabIndex = 8;
            this.textBox31.TextChanged += new System.EventHandler(this.textBox31_TextChanged);
            this.textBox31.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox31_KeyDown);
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label66.Location = new System.Drawing.Point(1, 14);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(39, 20);
            this.label66.TabIndex = 3;
            this.label66.Text = "Код";
            // 
            // textBox32
            // 
            this.textBox32.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox32.Location = new System.Drawing.Point(5, 37);
            this.textBox32.Name = "textBox32";
            this.textBox32.Size = new System.Drawing.Size(235, 26);
            this.textBox32.TabIndex = 2;
            this.textBox32.TextChanged += new System.EventHandler(this.textBox32_TextChanged);
            this.textBox32.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox32_KeyDown);
            // 
            // dataGridView5
            // 
            this.dataGridView5.AllowUserToAddRows = false;
            this.dataGridView5.AllowUserToDeleteRows = false;
            this.dataGridView5.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView5.Location = new System.Drawing.Point(245, 0);
            this.dataGridView5.MultiSelect = false;
            this.dataGridView5.Name = "dataGridView5";
            this.dataGridView5.ReadOnly = true;
            this.dataGridView5.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView5.Size = new System.Drawing.Size(636, 547);
            this.dataGridView5.TabIndex = 3;
            this.dataGridView5.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView5_CellDoubleClick);
            this.dataGridView5.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView5_KeyDown);
            // 
            // tabPage10
            // 
            this.tabPage10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tabPage10.Controls.Add(this.monthCalendar2);
            this.tabPage10.Controls.Add(this.button13);
            this.tabPage10.Controls.Add(this.dataGridView6);
            this.tabPage10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.tabPage10.Location = new System.Drawing.Point(4, 22);
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.Size = new System.Drawing.Size(881, 543);
            this.tabPage10.TabIndex = 9;
            this.tabPage10.Text = "Просмотр праздн";
            // 
            // monthCalendar2
            // 
            this.monthCalendar2.Location = new System.Drawing.Point(708, 248);
            this.monthCalendar2.Name = "monthCalendar2";
            this.monthCalendar2.TabIndex = 2;
            // 
            // button13
            // 
            this.button13.BackColor = System.Drawing.Color.White;
            this.button13.Location = new System.Drawing.Point(711, 16);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(149, 48);
            this.button13.TabIndex = 1;
            this.button13.Text = "Удалить";
            this.button13.UseVisualStyleBackColor = false;
            this.button13.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button13_MouseClick);
            this.button13.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.button13_PreviewKeyDown);
            // 
            // dataGridView6
            // 
            this.dataGridView6.AllowUserToAddRows = false;
            this.dataGridView6.AllowUserToDeleteRows = false;
            this.dataGridView6.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView6.Location = new System.Drawing.Point(0, 0);
            this.dataGridView6.MultiSelect = false;
            this.dataGridView6.Name = "dataGridView6";
            this.dataGridView6.ReadOnly = true;
            this.dataGridView6.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView6.Size = new System.Drawing.Size(705, 543);
            this.dataGridView6.TabIndex = 0;
            this.dataGridView6.SelectionChanged += new System.EventHandler(this.dataGridView6_SelectionChanged);
            this.dataGridView6.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView6_KeyDown);
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tabPage3.Controls.Add(this.button22);
            this.tabPage3.Controls.Add(this.dataGridView1);
            this.tabPage3.Controls.Add(this.panel17);
            this.tabPage3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(881, 543);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Просмотр опзд";
            // 
            // button22
            // 
            this.button22.BackColor = System.Drawing.Color.White;
            this.button22.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button22.Location = new System.Drawing.Point(2, 0);
            this.button22.Name = "button22";
            this.button22.Size = new System.Drawing.Size(17, 30);
            this.button22.TabIndex = 28;
            this.button22.Text = "<";
            this.button22.UseVisualStyleBackColor = false;
            this.button22.Click += new System.EventHandler(this.button22_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(242, -3);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(636, 547);
            this.dataGridView1.TabIndex = 15;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown_1);
            // 
            // panel17
            // 
            this.panel17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel17.Controls.Add(this.label63);
            this.panel17.Controls.Add(this.button26);
            this.panel17.Controls.Add(this.button23);
            this.panel17.Controls.Add(this.panel18);
            this.panel17.Controls.Add(this.checkBox3);
            this.panel17.Controls.Add(this.textBox33);
            this.panel17.Controls.Add(this.textBox34);
            this.panel17.Controls.Add(this.label71);
            this.panel17.Location = new System.Drawing.Point(-1, 0);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(246, 542);
            this.panel17.TabIndex = 6;
            this.panel17.Visible = false;
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label63.Location = new System.Drawing.Point(4, 80);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(140, 17);
            this.label63.TabIndex = 31;
            this.label63.Text = "Код подразделения";
            // 
            // button26
            // 
            this.button26.BackColor = System.Drawing.Color.White;
            this.button26.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button26.Location = new System.Drawing.Point(224, 100);
            this.button26.Name = "button26";
            this.button26.Size = new System.Drawing.Size(19, 26);
            this.button26.TabIndex = 30;
            this.button26.Text = "^";
            this.button26.UseVisualStyleBackColor = false;
            this.button26.Click += new System.EventHandler(this.button26_Click);
            // 
            // button23
            // 
            this.button23.BackColor = System.Drawing.Color.White;
            this.button23.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button23.Location = new System.Drawing.Point(226, 0);
            this.button23.Name = "button23";
            this.button23.Size = new System.Drawing.Size(17, 30);
            this.button23.TabIndex = 29;
            this.button23.Text = ">";
            this.button23.UseVisualStyleBackColor = false;
            this.button23.Click += new System.EventHandler(this.button23_Click);
            // 
            // panel18
            // 
            this.panel18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel18.Controls.Add(this.button33);
            this.panel18.Controls.Add(this.button32);
            this.panel18.Controls.Add(this.dateTimePicker7);
            this.panel18.Controls.Add(this.dateTimePicker8);
            this.panel18.Controls.Add(this.label67);
            this.panel18.Controls.Add(this.label68);
            this.panel18.Location = new System.Drawing.Point(2, 177);
            this.panel18.Name = "panel18";
            this.panel18.Size = new System.Drawing.Size(241, 129);
            this.panel18.TabIndex = 14;
            // 
            // button33
            // 
            this.button33.BackColor = System.Drawing.Color.White;
            this.button33.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button33.Location = new System.Drawing.Point(222, 79);
            this.button33.Name = "button33";
            this.button33.Size = new System.Drawing.Size(19, 26);
            this.button33.TabIndex = 31;
            this.button33.Text = "^";
            this.button33.UseVisualStyleBackColor = false;
            this.button33.Click += new System.EventHandler(this.button33_Click);
            // 
            // button32
            // 
            this.button32.BackColor = System.Drawing.Color.White;
            this.button32.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button32.Location = new System.Drawing.Point(222, 27);
            this.button32.Name = "button32";
            this.button32.Size = new System.Drawing.Size(19, 26);
            this.button32.TabIndex = 30;
            this.button32.Text = "^";
            this.button32.UseVisualStyleBackColor = false;
            this.button32.Click += new System.EventHandler(this.button32_Click);
            // 
            // dateTimePicker7
            // 
            this.dateTimePicker7.Location = new System.Drawing.Point(5, 27);
            this.dateTimePicker7.Name = "dateTimePicker7";
            this.dateTimePicker7.Size = new System.Drawing.Size(214, 26);
            this.dateTimePicker7.TabIndex = 5;
            this.dateTimePicker7.ValueChanged += new System.EventHandler(this.dateTimePicker7_ValueChanged);
            this.dateTimePicker7.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dateTimePicker7_KeyDown);
            // 
            // dateTimePicker8
            // 
            this.dateTimePicker8.Location = new System.Drawing.Point(5, 79);
            this.dateTimePicker8.Name = "dateTimePicker8";
            this.dateTimePicker8.Size = new System.Drawing.Size(214, 26);
            this.dateTimePicker8.TabIndex = 4;
            this.dateTimePicker8.ValueChanged += new System.EventHandler(this.dateTimePicker8_ValueChanged);
            this.dateTimePicker8.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dateTimePicker8_KeyDown);
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label67.Location = new System.Drawing.Point(1, 4);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(30, 20);
            this.label67.TabIndex = 6;
            this.label67.Text = "От";
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label68.Location = new System.Drawing.Point(1, 56);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(30, 20);
            this.label68.TabIndex = 7;
            this.label68.Text = "До";
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(6, 147);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(199, 24);
            this.checkBox3.TabIndex = 12;
            this.checkBox3.Text = "Учитывать дату срока";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            this.checkBox3.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.checkBox3_PreviewKeyDown);
            // 
            // textBox33
            // 
            this.textBox33.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox33.Location = new System.Drawing.Point(6, 35);
            this.textBox33.Name = "textBox33";
            this.textBox33.Size = new System.Drawing.Size(235, 26);
            this.textBox33.TabIndex = 10;
            this.textBox33.TextChanged += new System.EventHandler(this.textBox33_TextChanged);
            this.textBox33.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox33_KeyDown);
            // 
            // textBox34
            // 
            this.textBox34.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox34.Location = new System.Drawing.Point(7, 100);
            this.textBox34.Name = "textBox34";
            this.textBox34.Size = new System.Drawing.Size(214, 26);
            this.textBox34.TabIndex = 8;
            this.textBox34.TextChanged += new System.EventHandler(this.textBox34_TextChanged);
            this.textBox34.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox34_KeyDown);
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label71.Location = new System.Drawing.Point(3, 12);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(39, 20);
            this.label71.TabIndex = 3;
            this.label71.Text = "Код";
            // 
            // tabPage5
            // 
            this.tabPage5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tabPage5.Controls.Add(this.label1);
            this.tabPage5.Controls.Add(this.panel1);
            this.tabPage5.Controls.Add(this.panel2);
            this.tabPage5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(881, 543);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Добавление задачи";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Uighur", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(354, 38);
            this.label1.TabIndex = 1;
            this.label1.Text = "Информационная карта";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label24);
            this.panel1.Controls.Add(this.label23);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.textBox4);
            this.panel1.Controls.Add(this.textBox5);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.textBox6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.textBox3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Location = new System.Drawing.Point(6, 48);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(631, 292);
            this.panel1.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Uighur", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(15, 188);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(161, 26);
            this.label7.TabIndex = 20;
            this.label7.Text = "Подразделение";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Uighur", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(223, 188);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 26);
            this.label8.TabIndex = 19;
            this.label8.Text = "Фамилия";
            // 
            // label24
            // 
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label24.ForeColor = System.Drawing.Color.Red;
            this.label24.Location = new System.Drawing.Point(17, 247);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(185, 45);
            this.label24.TabIndex = 18;
            this.label24.Text = "Ошибка";
            this.label24.Visible = false;
            // 
            // label23
            // 
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label23.ForeColor = System.Drawing.Color.Red;
            this.label23.Location = new System.Drawing.Point(17, 115);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(185, 45);
            this.label23.TabIndex = 17;
            this.label23.Text = "Ошибка";
            this.label23.Visible = false;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.White;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button3.Location = new System.Drawing.Point(203, 217);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(19, 26);
            this.button3.TabIndex = 16;
            this.button3.Text = "^";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.Location = new System.Drawing.Point(203, 86);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(19, 26);
            this.button2.TabIndex = 15;
            this.button2.Text = "^";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Uighur", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(428, 188);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 26);
            this.label6.TabIndex = 14;
            this.label6.Text = "Таб.номер";
            // 
            // textBox4
            // 
            this.textBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox4.Location = new System.Drawing.Point(20, 217);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(182, 26);
            this.textBox4.TabIndex = 13;
            this.textBox4.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            this.textBox4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox4_KeyDown);
            // 
            // textBox5
            // 
            this.textBox5.Enabled = false;
            this.textBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox5.Location = new System.Drawing.Point(228, 217);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(182, 26);
            this.textBox5.TabIndex = 11;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Uighur", 20F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(236, 141);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(148, 34);
            this.label9.TabIndex = 9;
            this.label9.Text = "Контролёр";
            // 
            // textBox6
            // 
            this.textBox6.Enabled = false;
            this.textBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox6.Location = new System.Drawing.Point(433, 217);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(182, 26);
            this.textBox6.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Uighur", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(428, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 26);
            this.label5.TabIndex = 7;
            this.label5.Text = "Таб.номер";
            // 
            // textBox3
            // 
            this.textBox3.Enabled = false;
            this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox3.Location = new System.Drawing.Point(433, 86);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(182, 26);
            this.textBox3.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Uighur", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(15, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(161, 26);
            this.label4.TabIndex = 5;
            this.label4.Text = "Подразделение";
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox2.Location = new System.Drawing.Point(228, 86);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(182, 26);
            this.textBox2.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Uighur", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(223, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 26);
            this.label3.TabIndex = 3;
            this.label3.Text = "Фамилия";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Uighur", 20F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(236, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(174, 34);
            this.label2.TabIndex = 2;
            this.label2.Text = "Исполнитель";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox1.Location = new System.Drawing.Point(20, 86);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(182, 26);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel2.Controls.Add(this.checkBox8);
            this.panel2.Controls.Add(this.button14);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.label26);
            this.panel2.Controls.Add(this.dateTimePicker1);
            this.panel2.Controls.Add(this.label25);
            this.panel2.Controls.Add(this.textBox7);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Location = new System.Drawing.Point(3, 346);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(858, 138);
            this.panel2.TabIndex = 21;
            // 
            // checkBox8
            // 
            this.checkBox8.AutoSize = true;
            this.checkBox8.Location = new System.Drawing.Point(599, 81);
            this.checkBox8.Name = "checkBox8";
            this.checkBox8.Size = new System.Drawing.Size(136, 24);
            this.checkBox8.TabIndex = 39;
            this.checkBox8.Text = "Двусторонний";
            this.checkBox8.UseVisualStyleBackColor = true;
            this.checkBox8.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.checkBox8_PreviewKeyDown);
            // 
            // button14
            // 
            this.button14.BackColor = System.Drawing.Color.White;
            this.button14.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button14.Location = new System.Drawing.Point(558, 41);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(19, 26);
            this.button14.TabIndex = 24;
            this.button14.Text = "^";
            this.button14.UseVisualStyleBackColor = false;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.CausesValidation = false;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(599, 34);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(151, 41);
            this.button1.TabIndex = 0;
            this.button1.Text = "Добавить";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button1_MouseClick);
            this.button1.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.button1_PreviewKeyDown);
            // 
            // label26
            // 
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label26.ForeColor = System.Drawing.Color.Red;
            this.label26.Location = new System.Drawing.Point(314, 72);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(238, 57);
            this.label26.TabIndex = 23;
            this.label26.Text = "Ошибка";
            this.label26.Visible = false;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePicker1.Location = new System.Drawing.Point(317, 39);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(235, 29);
            this.dateTimePicker1.TabIndex = 22;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            this.dateTimePicker1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dateTimePicker1_KeyDown);
            // 
            // label25
            // 
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label25.ForeColor = System.Drawing.Color.Red;
            this.label25.Location = new System.Drawing.Point(5, 72);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(282, 57);
            this.label25.TabIndex = 19;
            this.label25.Text = "Ошибка";
            this.label25.Visible = false;
            // 
            // textBox7
            // 
            this.textBox7.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox7.Location = new System.Drawing.Point(8, 39);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(279, 30);
            this.textBox7.TabIndex = 15;
            this.textBox7.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox7_KeyDown);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Uighur", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(3, 10);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(129, 26);
            this.label10.TabIndex = 16;
            this.label10.Text = "Код задания";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Uighur", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(332, 10);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(169, 26);
            this.label11.TabIndex = 18;
            this.label11.Text = "Срок исполнения";
            // 
            // tabPage6
            // 
            this.tabPage6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tabPage6.Controls.Add(this.panel6);
            this.tabPage6.Controls.Add(this.panel5);
            this.tabPage6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(881, 543);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Добавление Исп-Кон";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Gray;
            this.panel6.Controls.Add(this.button4);
            this.panel6.Location = new System.Drawing.Point(276, 328);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(265, 61);
            this.panel6.TabIndex = 2;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.White;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button4.Location = new System.Drawing.Point(3, 3);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(260, 54);
            this.button4.TabIndex = 0;
            this.button4.Text = "Добавить Контролёра/исполнителя";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button4_MouseClick);
            this.button4.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.button4_PreviewKeyDown);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Silver;
            this.panel5.Controls.Add(this.label22);
            this.panel5.Controls.Add(this.label21);
            this.panel5.Controls.Add(this.label20);
            this.panel5.Controls.Add(this.label19);
            this.panel5.Controls.Add(this.label18);
            this.panel5.Controls.Add(this.label16);
            this.panel5.Controls.Add(this.textBox12);
            this.panel5.Controls.Add(this.label15);
            this.panel5.Controls.Add(this.textBox11);
            this.panel5.Controls.Add(this.label14);
            this.panel5.Controls.Add(this.textBox10);
            this.panel5.Controls.Add(this.label13);
            this.panel5.Controls.Add(this.textBox9);
            this.panel5.Controls.Add(this.label12);
            this.panel5.Controls.Add(this.textBox8);
            this.panel5.Controls.Add(this.pictureBox2);
            this.panel5.Location = new System.Drawing.Point(14, 11);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(527, 311);
            this.panel5.TabIndex = 1;
            // 
            // label22
            // 
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label22.ForeColor = System.Drawing.Color.Red;
            this.label22.Location = new System.Drawing.Point(345, 236);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(165, 67);
            this.label22.TabIndex = 15;
            this.label22.Text = "Ошибка";
            this.label22.Visible = false;
            // 
            // label21
            // 
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label21.ForeColor = System.Drawing.Color.Red;
            this.label21.Location = new System.Drawing.Point(204, 236);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(125, 67);
            this.label21.TabIndex = 14;
            this.label21.Text = "Ошибка";
            this.label21.Visible = false;
            // 
            // label20
            // 
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label20.ForeColor = System.Drawing.Color.Red;
            this.label20.Location = new System.Drawing.Point(204, 174);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(306, 13);
            this.label20.TabIndex = 13;
            this.label20.Text = "Ошибка";
            this.label20.Visible = false;
            // 
            // label19
            // 
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Location = new System.Drawing.Point(204, 116);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(306, 13);
            this.label19.TabIndex = 12;
            this.label19.Text = "Ошибка";
            this.label19.Visible = false;
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label18.ForeColor = System.Drawing.Color.Red;
            this.label18.Location = new System.Drawing.Point(204, 57);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(306, 13);
            this.label18.TabIndex = 11;
            this.label18.Text = "Ошибка";
            this.label18.Visible = false;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label16.Location = new System.Drawing.Point(345, 187);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(140, 17);
            this.label16.TabIndex = 10;
            this.label16.Text = "Код подразделения";
            // 
            // textBox12
            // 
            this.textBox12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox12.Location = new System.Drawing.Point(348, 207);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(162, 26);
            this.textBox12.TabIndex = 9;
            this.textBox12.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox12_KeyDown);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label15.Location = new System.Drawing.Point(201, 187);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(33, 17);
            this.label15.TabIndex = 8;
            this.label15.Text = "Код";
            // 
            // textBox11
            // 
            this.textBox11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox11.Location = new System.Drawing.Point(204, 207);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(125, 26);
            this.textBox11.TabIndex = 7;
            this.textBox11.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox11_KeyDown);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label14.Location = new System.Drawing.Point(201, 129);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(71, 17);
            this.label14.TabIndex = 6;
            this.label14.Text = "Отчество";
            // 
            // textBox10
            // 
            this.textBox10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox10.Location = new System.Drawing.Point(204, 149);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(306, 26);
            this.textBox10.TabIndex = 5;
            this.textBox10.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox10_KeyDown);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label13.Location = new System.Drawing.Point(201, 70);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(35, 17);
            this.label13.TabIndex = 4;
            this.label13.Text = "Имя";
            // 
            // textBox9
            // 
            this.textBox9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox9.Location = new System.Drawing.Point(204, 90);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(306, 26);
            this.textBox9.TabIndex = 3;
            this.textBox9.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox9_KeyDown);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.Location = new System.Drawing.Point(201, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(70, 17);
            this.label12.TabIndex = 2;
            this.label12.Text = "Фамилия";
            // 
            // textBox8
            // 
            this.textBox8.BackColor = System.Drawing.Color.White;
            this.textBox8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox8.Location = new System.Drawing.Point(204, 29);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(306, 26);
            this.textBox8.TabIndex = 1;
            this.textBox8.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox8_KeyDown);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(3, 9);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(195, 202);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // tabPage7
            // 
            this.tabPage7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tabPage7.Controls.Add(this.label27);
            this.tabPage7.Controls.Add(this.label17);
            this.tabPage7.Controls.Add(this.monthCalendar1);
            this.tabPage7.Controls.Add(this.button5);
            this.tabPage7.Controls.Add(this.dataGridView2);
            this.tabPage7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(881, 543);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "Добавление празн";
            // 
            // label27
            // 
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label27.ForeColor = System.Drawing.Color.Red;
            this.label27.Location = new System.Drawing.Point(361, 172);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(164, 49);
            this.label27.TabIndex = 15;
            this.label27.Text = "Ошибка";
            this.label27.Visible = false;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label17.Location = new System.Drawing.Point(528, 9);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(75, 25);
            this.label17.TabIndex = 4;
            this.label17.Text = "label17";
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.FirstDayOfWeek = System.Windows.Forms.Day.Monday;
            this.monthCalendar1.Location = new System.Drawing.Point(361, 9);
            this.monthCalendar1.MaxSelectionCount = 100;
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.ShowTodayCircle = false;
            this.monthCalendar1.TabIndex = 3;
            this.monthCalendar1.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateChanged);
            this.monthCalendar1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.monthCalendar1_KeyDown);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.White;
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button5.Location = new System.Drawing.Point(361, 236);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(171, 39);
            this.button5.TabIndex = 2;
            this.button5.Text = "Добавить";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button5_MouseClick);
            this.button5.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.button5_PreviewKeyDown);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(0, 0);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(349, 541);
            this.dataGridView2.TabIndex = 0;
            // 
            // IspCon
            // 
            this.IspCon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.IspCon.Controls.Add(this.dataGridView4);
            this.IspCon.Controls.Add(this.panel8);
            this.IspCon.Controls.Add(this.panel7);
            this.IspCon.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.IspCon.Location = new System.Drawing.Point(4, 22);
            this.IspCon.Name = "IspCon";
            this.IspCon.Size = new System.Drawing.Size(881, 543);
            this.IspCon.TabIndex = 7;
            this.IspCon.Text = "Исп-Кон";
            // 
            // dataGridView4
            // 
            this.dataGridView4.AllowUserToAddRows = false;
            this.dataGridView4.AllowUserToDeleteRows = false;
            this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView4.Location = new System.Drawing.Point(3, 339);
            this.dataGridView4.MultiSelect = false;
            this.dataGridView4.Name = "dataGridView4";
            this.dataGridView4.ReadOnly = true;
            this.dataGridView4.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView4.Size = new System.Drawing.Size(878, 208);
            this.dataGridView4.TabIndex = 4;
            this.dataGridView4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView4_KeyDown);
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.DarkGray;
            this.panel8.Controls.Add(this.button7);
            this.panel8.Controls.Add(this.button6);
            this.panel8.Location = new System.Drawing.Point(555, 58);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(166, 182);
            this.panel8.TabIndex = 3;
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.White;
            this.button7.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button7.Location = new System.Drawing.Point(13, 100);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(141, 46);
            this.button7.TabIndex = 1;
            this.button7.Text = "Удалить";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button7_MouseClick);
            this.button7.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.button7_PreviewKeyDown);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.White;
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button6.Location = new System.Drawing.Point(13, 23);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(141, 46);
            this.button6.TabIndex = 0;
            this.button6.Text = "Обновить";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button6_MouseClick);
            this.button6.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.button6_PreviewKeyDown);
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Silver;
            this.panel7.Controls.Add(this.label28);
            this.panel7.Controls.Add(this.label29);
            this.panel7.Controls.Add(this.label30);
            this.panel7.Controls.Add(this.label31);
            this.panel7.Controls.Add(this.label32);
            this.panel7.Controls.Add(this.label33);
            this.panel7.Controls.Add(this.textBox14);
            this.panel7.Controls.Add(this.label34);
            this.panel7.Controls.Add(this.textBox15);
            this.panel7.Controls.Add(this.label35);
            this.panel7.Controls.Add(this.textBox16);
            this.panel7.Controls.Add(this.label36);
            this.panel7.Controls.Add(this.textBox17);
            this.panel7.Controls.Add(this.label37);
            this.panel7.Controls.Add(this.textBox18);
            this.panel7.Controls.Add(this.pictureBox3);
            this.panel7.Location = new System.Drawing.Point(3, 12);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(527, 311);
            this.panel7.TabIndex = 2;
            // 
            // label28
            // 
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label28.ForeColor = System.Drawing.Color.Red;
            this.label28.Location = new System.Drawing.Point(345, 236);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(165, 67);
            this.label28.TabIndex = 15;
            this.label28.Text = "Ошибка";
            this.label28.Visible = false;
            // 
            // label29
            // 
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label29.ForeColor = System.Drawing.Color.Red;
            this.label29.Location = new System.Drawing.Point(204, 236);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(125, 67);
            this.label29.TabIndex = 14;
            this.label29.Text = "Ошибка";
            this.label29.Visible = false;
            // 
            // label30
            // 
            this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label30.ForeColor = System.Drawing.Color.Red;
            this.label30.Location = new System.Drawing.Point(204, 174);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(306, 13);
            this.label30.TabIndex = 13;
            this.label30.Text = "Ошибка";
            this.label30.Visible = false;
            // 
            // label31
            // 
            this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label31.ForeColor = System.Drawing.Color.Red;
            this.label31.Location = new System.Drawing.Point(204, 116);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(306, 13);
            this.label31.TabIndex = 12;
            this.label31.Text = "Ошибка";
            this.label31.Visible = false;
            // 
            // label32
            // 
            this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label32.ForeColor = System.Drawing.Color.Red;
            this.label32.Location = new System.Drawing.Point(204, 57);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(306, 13);
            this.label32.TabIndex = 11;
            this.label32.Text = "Ошибка";
            this.label32.Visible = false;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label33.Location = new System.Drawing.Point(345, 187);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(140, 17);
            this.label33.TabIndex = 10;
            this.label33.Text = "Код подразделения";
            // 
            // textBox14
            // 
            this.textBox14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox14.Location = new System.Drawing.Point(348, 207);
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new System.Drawing.Size(162, 26);
            this.textBox14.TabIndex = 9;
            this.textBox14.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox14_KeyDown);
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label34.Location = new System.Drawing.Point(201, 187);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(33, 17);
            this.label34.TabIndex = 8;
            this.label34.Text = "Код";
            // 
            // textBox15
            // 
            this.textBox15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox15.Location = new System.Drawing.Point(204, 207);
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new System.Drawing.Size(125, 26);
            this.textBox15.TabIndex = 7;
            this.textBox15.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox15_KeyDown);
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label35.Location = new System.Drawing.Point(201, 129);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(71, 17);
            this.label35.TabIndex = 6;
            this.label35.Text = "Отчество";
            // 
            // textBox16
            // 
            this.textBox16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox16.Location = new System.Drawing.Point(204, 149);
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new System.Drawing.Size(306, 26);
            this.textBox16.TabIndex = 5;
            this.textBox16.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox16_KeyDown);
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label36.Location = new System.Drawing.Point(201, 70);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(35, 17);
            this.label36.TabIndex = 4;
            this.label36.Text = "Имя";
            // 
            // textBox17
            // 
            this.textBox17.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox17.Location = new System.Drawing.Point(204, 90);
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new System.Drawing.Size(306, 26);
            this.textBox17.TabIndex = 3;
            this.textBox17.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox17_KeyDown);
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label37.Location = new System.Drawing.Point(201, 9);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(70, 17);
            this.label37.TabIndex = 2;
            this.label37.Text = "Фамилия";
            // 
            // textBox18
            // 
            this.textBox18.BackColor = System.Drawing.Color.White;
            this.textBox18.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox18.Location = new System.Drawing.Point(204, 29);
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new System.Drawing.Size(306, 26);
            this.textBox18.TabIndex = 1;
            this.textBox18.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox18_KeyDown);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(3, 9);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(195, 202);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 0;
            this.pictureBox3.TabStop = false;
            // 
            // Tasks
            // 
            this.Tasks.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Tasks.Controls.Add(this.panel19);
            this.Tasks.Controls.Add(this.panel11);
            this.Tasks.Controls.Add(this.panel9);
            this.Tasks.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Tasks.Location = new System.Drawing.Point(4, 22);
            this.Tasks.Name = "Tasks";
            this.Tasks.Size = new System.Drawing.Size(881, 543);
            this.Tasks.TabIndex = 10;
            this.Tasks.Text = "Задачи";
            // 
            // panel19
            // 
            this.panel19.BackColor = System.Drawing.Color.Silver;
            this.panel19.Controls.Add(this.checkBox7);
            this.panel19.Controls.Add(this.label57);
            this.panel19.Controls.Add(this.textBox28);
            this.panel19.Controls.Add(this.label52);
            this.panel19.Controls.Add(this.button27);
            this.panel19.Controls.Add(this.dateTimePicker9);
            this.panel19.Controls.Add(this.label38);
            this.panel19.Controls.Add(this.label40);
            this.panel19.Location = new System.Drawing.Point(184, 365);
            this.panel19.Name = "panel19";
            this.panel19.Size = new System.Drawing.Size(455, 127);
            this.panel19.TabIndex = 35;
            // 
            // checkBox7
            // 
            this.checkBox7.AutoSize = true;
            this.checkBox7.Location = new System.Drawing.Point(316, 100);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(136, 24);
            this.checkBox7.TabIndex = 38;
            this.checkBox7.Text = "Двусторонний";
            this.checkBox7.UseVisualStyleBackColor = true;
            this.checkBox7.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.checkBox7_PreviewKeyDown);
            // 
            // label57
            // 
            this.label57.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label57.ForeColor = System.Drawing.Color.Red;
            this.label57.Location = new System.Drawing.Point(294, 66);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(145, 31);
            this.label57.TabIndex = 37;
            this.label57.Text = "Ошибка";
            this.label57.Visible = false;
            // 
            // textBox28
            // 
            this.textBox28.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox28.Location = new System.Drawing.Point(297, 37);
            this.textBox28.Name = "textBox28";
            this.textBox28.Size = new System.Drawing.Size(142, 26);
            this.textBox28.TabIndex = 36;
            this.textBox28.TextChanged += new System.EventHandler(this.textBox28_TextChanged);
            this.textBox28.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox28_KeyDown);
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Font = new System.Drawing.Font("Microsoft Uighur", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.Location = new System.Drawing.Point(292, 8);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(80, 26);
            this.label52.TabIndex = 35;
            this.label52.Text = "Оценка";
            // 
            // button27
            // 
            this.button27.BackColor = System.Drawing.Color.White;
            this.button27.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button27.Location = new System.Drawing.Point(251, 39);
            this.button27.Name = "button27";
            this.button27.Size = new System.Drawing.Size(19, 26);
            this.button27.TabIndex = 34;
            this.button27.Text = "^";
            this.button27.UseVisualStyleBackColor = false;
            this.button27.Click += new System.EventHandler(this.button27_Click);
            // 
            // dateTimePicker9
            // 
            this.dateTimePicker9.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePicker9.Location = new System.Drawing.Point(10, 37);
            this.dateTimePicker9.Name = "dateTimePicker9";
            this.dateTimePicker9.Size = new System.Drawing.Size(235, 29);
            this.dateTimePicker9.TabIndex = 33;
            this.dateTimePicker9.ValueChanged += new System.EventHandler(this.dateTimePicker9_ValueChanged);
            this.dateTimePicker9.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dateTimePicker9_KeyDown);
            // 
            // label38
            // 
            this.label38.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label38.ForeColor = System.Drawing.Color.Red;
            this.label38.Location = new System.Drawing.Point(7, 70);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(238, 13);
            this.label38.TabIndex = 32;
            this.label38.Text = "Ошибка";
            this.label38.Visible = false;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("Microsoft Uighur", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.Location = new System.Drawing.Point(5, 12);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(150, 26);
            this.label40.TabIndex = 31;
            this.label40.Text = "Дата закрытия";
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.Color.Silver;
            this.panel11.Controls.Add(this.button48);
            this.panel11.Controls.Add(this.button47);
            this.panel11.Controls.Add(this.button12);
            this.panel11.Controls.Add(this.button11);
            this.panel11.Controls.Add(this.button10);
            this.panel11.Location = new System.Drawing.Point(645, 19);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(204, 340);
            this.panel11.TabIndex = 35;
            // 
            // button48
            // 
            this.button48.BackColor = System.Drawing.Color.White;
            this.button48.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button48.Location = new System.Drawing.Point(22, 289);
            this.button48.Name = "button48";
            this.button48.Size = new System.Drawing.Size(164, 43);
            this.button48.TabIndex = 4;
            this.button48.Text = "Рецензия";
            this.button48.UseVisualStyleBackColor = false;
            this.button48.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button48_MouseClick);
            this.button48.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.button48_PreviewKeyDown);
            // 
            // button47
            // 
            this.button47.BackColor = System.Drawing.Color.White;
            this.button47.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button47.Location = new System.Drawing.Point(22, 222);
            this.button47.Name = "button47";
            this.button47.Size = new System.Drawing.Size(164, 43);
            this.button47.TabIndex = 3;
            this.button47.Text = "Претензия";
            this.button47.UseVisualStyleBackColor = false;
            this.button47.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button47_MouseClick);
            this.button47.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.button47_PreviewKeyDown);
            // 
            // button12
            // 
            this.button12.BackColor = System.Drawing.Color.White;
            this.button12.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button12.Location = new System.Drawing.Point(22, 151);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(164, 43);
            this.button12.TabIndex = 2;
            this.button12.Text = "Удалить";
            this.button12.UseVisualStyleBackColor = false;
            this.button12.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button12_MouseClick);
            this.button12.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.button12_PreviewKeyDown);
            // 
            // button11
            // 
            this.button11.BackColor = System.Drawing.Color.White;
            this.button11.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button11.Location = new System.Drawing.Point(22, 79);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(164, 43);
            this.button11.TabIndex = 1;
            this.button11.Text = "Закрыть";
            this.button11.UseVisualStyleBackColor = false;
            this.button11.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button11_MouseClick);
            this.button11.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.button11_PreviewKeyDown);
            // 
            // button10
            // 
            this.button10.BackColor = System.Drawing.Color.White;
            this.button10.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button10.Location = new System.Drawing.Point(22, 11);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(164, 43);
            this.button10.TabIndex = 0;
            this.button10.Text = "Изменить";
            this.button10.UseVisualStyleBackColor = false;
            this.button10.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button10_MouseClick);
            this.button10.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.button10_PreviewKeyDown);
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.Silver;
            this.panel9.Controls.Add(this.button15);
            this.panel9.Controls.Add(this.label72);
            this.panel9.Controls.Add(this.label56);
            this.panel9.Controls.Add(this.label55);
            this.panel9.Controls.Add(this.label53);
            this.panel9.Controls.Add(this.label51);
            this.panel9.Controls.Add(this.dateTimePicker4);
            this.panel9.Controls.Add(this.label41);
            this.panel9.Controls.Add(this.label42);
            this.panel9.Controls.Add(this.textBox27);
            this.panel9.Controls.Add(this.button8);
            this.panel9.Controls.Add(this.button9);
            this.panel9.Controls.Add(this.label43);
            this.panel9.Controls.Add(this.textBox21);
            this.panel9.Controls.Add(this.label44);
            this.panel9.Controls.Add(this.textBox22);
            this.panel9.Controls.Add(this.label45);
            this.panel9.Controls.Add(this.label46);
            this.panel9.Controls.Add(this.textBox23);
            this.panel9.Controls.Add(this.label47);
            this.panel9.Controls.Add(this.textBox24);
            this.panel9.Controls.Add(this.label48);
            this.panel9.Controls.Add(this.textBox25);
            this.panel9.Controls.Add(this.label49);
            this.panel9.Controls.Add(this.label50);
            this.panel9.Controls.Add(this.textBox26);
            this.panel9.Location = new System.Drawing.Point(8, 13);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(631, 346);
            this.panel9.TabIndex = 1;
            // 
            // button15
            // 
            this.button15.BackColor = System.Drawing.Color.White;
            this.button15.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button15.Location = new System.Drawing.Point(567, 294);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(19, 26);
            this.button15.TabIndex = 33;
            this.button15.Text = "^";
            this.button15.UseVisualStyleBackColor = false;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.label72.ForeColor = System.Drawing.Color.Red;
            this.label72.Location = new System.Drawing.Point(-1, 0);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(180, 24);
            this.label72.TabIndex = 32;
            this.label72.Text = "Архивная запись";
            // 
            // label56
            // 
            this.label56.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label56.ForeColor = System.Drawing.Color.Red;
            this.label56.Location = new System.Drawing.Point(323, 324);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(238, 13);
            this.label56.TabIndex = 31;
            this.label56.Text = "Ошибка";
            this.label56.Visible = false;
            // 
            // label55
            // 
            this.label55.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label55.ForeColor = System.Drawing.Color.Red;
            this.label55.Location = new System.Drawing.Point(17, 325);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(279, 13);
            this.label55.TabIndex = 30;
            this.label55.Text = "Ошибка";
            this.label55.Visible = false;
            // 
            // label53
            // 
            this.label53.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label53.ForeColor = System.Drawing.Color.Red;
            this.label53.Location = new System.Drawing.Point(17, 246);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(185, 13);
            this.label53.TabIndex = 29;
            this.label53.Text = "Ошибка";
            this.label53.Visible = false;
            // 
            // label51
            // 
            this.label51.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label51.ForeColor = System.Drawing.Color.Red;
            this.label51.Location = new System.Drawing.Point(17, 115);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(185, 13);
            this.label51.TabIndex = 28;
            this.label51.Text = "Ошибка";
            this.label51.Visible = false;
            // 
            // dateTimePicker4
            // 
            this.dateTimePicker4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePicker4.Location = new System.Drawing.Point(326, 292);
            this.dateTimePicker4.Name = "dateTimePicker4";
            this.dateTimePicker4.Size = new System.Drawing.Size(235, 29);
            this.dateTimePicker4.TabIndex = 26;
            this.dateTimePicker4.ValueChanged += new System.EventHandler(this.dateTimePicker4_ValueChanged);
            this.dateTimePicker4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dateTimePicker4_KeyDown);
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Font = new System.Drawing.Font("Microsoft Uighur", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.Location = new System.Drawing.Point(341, 263);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(169, 26);
            this.label41.TabIndex = 25;
            this.label41.Text = "Срок исполнения";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("Microsoft Uighur", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.Location = new System.Drawing.Point(15, 263);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(129, 26);
            this.label42.TabIndex = 24;
            this.label42.Text = "Код задания";
            // 
            // textBox27
            // 
            this.textBox27.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox27.Location = new System.Drawing.Point(20, 292);
            this.textBox27.Name = "textBox27";
            this.textBox27.Size = new System.Drawing.Size(276, 30);
            this.textBox27.TabIndex = 23;
            this.textBox27.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox27_KeyDown);
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.White;
            this.button8.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button8.Location = new System.Drawing.Point(203, 217);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(19, 26);
            this.button8.TabIndex = 16;
            this.button8.Text = "^";
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.Color.White;
            this.button9.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button9.Location = new System.Drawing.Point(203, 86);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(19, 26);
            this.button9.TabIndex = 15;
            this.button9.Text = "^";
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Font = new System.Drawing.Font("Microsoft Uighur", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.Location = new System.Drawing.Point(428, 188);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(105, 26);
            this.label43.TabIndex = 14;
            this.label43.Text = "Таб.номер";
            // 
            // textBox21
            // 
            this.textBox21.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox21.Location = new System.Drawing.Point(20, 217);
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new System.Drawing.Size(182, 26);
            this.textBox21.TabIndex = 13;
            this.textBox21.TextChanged += new System.EventHandler(this.textBox21_TextChanged);
            this.textBox21.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox21_KeyDown);
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Font = new System.Drawing.Font("Microsoft Uighur", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.Location = new System.Drawing.Point(18, 188);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(161, 26);
            this.label44.TabIndex = 12;
            this.label44.Text = "Подразделение";
            // 
            // textBox22
            // 
            this.textBox22.Enabled = false;
            this.textBox22.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox22.Location = new System.Drawing.Point(228, 217);
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new System.Drawing.Size(182, 26);
            this.textBox22.TabIndex = 11;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Font = new System.Drawing.Font("Microsoft Uighur", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.Location = new System.Drawing.Point(228, 188);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(97, 26);
            this.label45.TabIndex = 10;
            this.label45.Text = "Фамилия";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Font = new System.Drawing.Font("Microsoft Uighur", 20F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label46.Location = new System.Drawing.Point(236, 141);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(148, 34);
            this.label46.TabIndex = 9;
            this.label46.Text = "Контролёр";
            // 
            // textBox23
            // 
            this.textBox23.Enabled = false;
            this.textBox23.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox23.Location = new System.Drawing.Point(433, 217);
            this.textBox23.Name = "textBox23";
            this.textBox23.Size = new System.Drawing.Size(182, 26);
            this.textBox23.TabIndex = 8;
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Font = new System.Drawing.Font("Microsoft Uighur", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label47.Location = new System.Drawing.Point(428, 57);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(105, 26);
            this.label47.TabIndex = 7;
            this.label47.Text = "Таб.номер";
            // 
            // textBox24
            // 
            this.textBox24.Enabled = false;
            this.textBox24.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox24.Location = new System.Drawing.Point(433, 86);
            this.textBox24.Name = "textBox24";
            this.textBox24.Size = new System.Drawing.Size(182, 26);
            this.textBox24.TabIndex = 6;
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Font = new System.Drawing.Font("Microsoft Uighur", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label48.Location = new System.Drawing.Point(15, 57);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(161, 26);
            this.label48.TabIndex = 5;
            this.label48.Text = "Подразделение";
            // 
            // textBox25
            // 
            this.textBox25.Enabled = false;
            this.textBox25.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox25.Location = new System.Drawing.Point(228, 86);
            this.textBox25.Name = "textBox25";
            this.textBox25.Size = new System.Drawing.Size(182, 26);
            this.textBox25.TabIndex = 4;
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Font = new System.Drawing.Font("Microsoft Uighur", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label49.Location = new System.Drawing.Point(228, 57);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(97, 26);
            this.label49.TabIndex = 3;
            this.label49.Text = "Фамилия";
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Font = new System.Drawing.Font("Microsoft Uighur", 20F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label50.Location = new System.Drawing.Point(236, 10);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(174, 34);
            this.label50.TabIndex = 2;
            this.label50.Text = "Исполнитель";
            // 
            // textBox26
            // 
            this.textBox26.BackColor = System.Drawing.Color.White;
            this.textBox26.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox26.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox26.Location = new System.Drawing.Point(20, 86);
            this.textBox26.Name = "textBox26";
            this.textBox26.Size = new System.Drawing.Size(182, 26);
            this.textBox26.TabIndex = 0;
            this.textBox26.TextChanged += new System.EventHandler(this.textBox26_TextChanged);
            this.textBox26.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox26_KeyDown);
            // 
            // tabPage8
            // 
            this.tabPage8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tabPage8.Controls.Add(this.panel23);
            this.tabPage8.Controls.Add(this.dataGridView7);
            this.tabPage8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabPage8.Location = new System.Drawing.Point(4, 22);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Size = new System.Drawing.Size(881, 543);
            this.tabPage8.TabIndex = 11;
            this.tabPage8.Text = "Печать списка";
            // 
            // panel23
            // 
            this.panel23.Controls.Add(this.textBox30);
            this.panel23.Controls.Add(this.button36);
            this.panel23.Controls.Add(this.button34);
            this.panel23.Controls.Add(this.textBox20);
            this.panel23.Controls.Add(this.label76);
            this.panel23.Controls.Add(this.label75);
            this.panel23.Controls.Add(this.dateTimePicker11);
            this.panel23.Controls.Add(this.dateTimePicker10);
            this.panel23.Controls.Add(this.label74);
            this.panel23.Location = new System.Drawing.Point(53, 3);
            this.panel23.Name = "panel23";
            this.panel23.Size = new System.Drawing.Size(777, 82);
            this.panel23.TabIndex = 34;
            // 
            // textBox30
            // 
            this.textBox30.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox30.Location = new System.Drawing.Point(238, 44);
            this.textBox30.Name = "textBox30";
            this.textBox30.Size = new System.Drawing.Size(95, 32);
            this.textBox30.TabIndex = 33;
            this.textBox30.TextChanged += new System.EventHandler(this.textBox30_TextChanged);
            this.textBox30.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox30_KeyDown);
            // 
            // button36
            // 
            this.button36.BackColor = System.Drawing.Color.White;
            this.button36.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button36.Location = new System.Drawing.Point(335, 44);
            this.button36.Name = "button36";
            this.button36.Size = new System.Drawing.Size(21, 29);
            this.button36.TabIndex = 32;
            this.button36.Text = "^";
            this.button36.UseVisualStyleBackColor = false;
            this.button36.Click += new System.EventHandler(this.button36_Click);
            // 
            // button34
            // 
            this.button34.BackColor = System.Drawing.Color.White;
            this.button34.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button34.Location = new System.Drawing.Point(648, 46);
            this.button34.Name = "button34";
            this.button34.Size = new System.Drawing.Size(127, 30);
            this.button34.TabIndex = 7;
            this.button34.Text = "Печать";
            this.button34.UseVisualStyleBackColor = false;
            this.button34.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button34_MouseClick);
            this.button34.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.button34_PreviewKeyDown);
            // 
            // textBox20
            // 
            this.textBox20.Enabled = false;
            this.textBox20.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox20.Location = new System.Drawing.Point(366, 44);
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new System.Drawing.Size(251, 32);
            this.textBox20.TabIndex = 5;
            // 
            // label76
            // 
            this.label76.AutoSize = true;
            this.label76.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label76.Location = new System.Drawing.Point(61, 44);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(171, 29);
            this.label76.TabIndex = 4;
            this.label76.Text = "Исполнитель";
            // 
            // label75
            // 
            this.label75.AutoSize = true;
            this.label75.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label75.Location = new System.Drawing.Point(418, 7);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(44, 29);
            this.label75.TabIndex = 3;
            this.label75.Text = "По";
            // 
            // dateTimePicker11
            // 
            this.dateTimePicker11.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePicker11.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker11.Location = new System.Drawing.Point(468, 6);
            this.dateTimePicker11.Name = "dateTimePicker11";
            this.dateTimePicker11.Size = new System.Drawing.Size(149, 32);
            this.dateTimePicker11.TabIndex = 2;
            this.dateTimePicker11.ValueChanged += new System.EventHandler(this.dateTimePicker11_ValueChanged);
            this.dateTimePicker11.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dateTimePicker11_KeyDown);
            // 
            // dateTimePicker10
            // 
            this.dateTimePicker10.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePicker10.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker10.Location = new System.Drawing.Point(263, 6);
            this.dateTimePicker10.Name = "dateTimePicker10";
            this.dateTimePicker10.Size = new System.Drawing.Size(149, 32);
            this.dateTimePicker10.TabIndex = 1;
            this.dateTimePicker10.ValueChanged += new System.EventHandler(this.dateTimePicker10_ValueChanged);
            this.dateTimePicker10.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dateTimePicker10_KeyDown);
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label74.Location = new System.Drawing.Point(7, 6);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(250, 29);
            this.label74.TabIndex = 0;
            this.label74.Text = "Перечень заданий с";
            // 
            // dataGridView7
            // 
            this.dataGridView7.AllowUserToAddRows = false;
            this.dataGridView7.AllowUserToDeleteRows = false;
            this.dataGridView7.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView7.Location = new System.Drawing.Point(0, 88);
            this.dataGridView7.MultiSelect = false;
            this.dataGridView7.Name = "dataGridView7";
            this.dataGridView7.ReadOnly = true;
            this.dataGridView7.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView7.Size = new System.Drawing.Size(881, 459);
            this.dataGridView7.TabIndex = 6;
            this.dataGridView7.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView7_KeyDown);
            // 
            // tabPage11
            // 
            this.tabPage11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tabPage11.Controls.Add(this.dataGridView8);
            this.tabPage11.Controls.Add(this.panel24);
            this.tabPage11.Location = new System.Drawing.Point(4, 22);
            this.tabPage11.Name = "tabPage11";
            this.tabPage11.Size = new System.Drawing.Size(881, 543);
            this.tabPage11.TabIndex = 12;
            this.tabPage11.Text = "Печать опазданий";
            // 
            // dataGridView8
            // 
            this.dataGridView8.AllowUserToAddRows = false;
            this.dataGridView8.AllowUserToDeleteRows = false;
            this.dataGridView8.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView8.Location = new System.Drawing.Point(0, 80);
            this.dataGridView8.MultiSelect = false;
            this.dataGridView8.Name = "dataGridView8";
            this.dataGridView8.ReadOnly = true;
            this.dataGridView8.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView8.Size = new System.Drawing.Size(881, 459);
            this.dataGridView8.TabIndex = 11;
            this.dataGridView8.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView8_KeyDown);
            // 
            // panel24
            // 
            this.panel24.Controls.Add(this.dateTimePicker19);
            this.panel24.Controls.Add(this.label78);
            this.panel24.Controls.Add(this.button37);
            this.panel24.Controls.Add(this.label77);
            this.panel24.Location = new System.Drawing.Point(198, 3);
            this.panel24.Name = "panel24";
            this.panel24.Size = new System.Drawing.Size(516, 75);
            this.panel24.TabIndex = 14;
            // 
            // dateTimePicker19
            // 
            this.dateTimePicker19.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePicker19.Location = new System.Drawing.Point(55, 40);
            this.dateTimePicker19.Name = "dateTimePicker19";
            this.dateTimePicker19.Size = new System.Drawing.Size(240, 32);
            this.dateTimePicker19.TabIndex = 13;
            this.dateTimePicker19.ValueChanged += new System.EventHandler(this.dateTimePicker19_ValueChanged);
            this.dateTimePicker19.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dateTimePicker19_KeyDown);
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label78.Location = new System.Drawing.Point(6, 3);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(364, 29);
            this.label78.TabIndex = 8;
            this.label78.Text = "Не выполнили задание в срок";
            // 
            // button37
            // 
            this.button37.BackColor = System.Drawing.Color.White;
            this.button37.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button37.Location = new System.Drawing.Point(383, 41);
            this.button37.Name = "button37";
            this.button37.Size = new System.Drawing.Size(127, 30);
            this.button37.TabIndex = 12;
            this.button37.Text = "Печать";
            this.button37.UseVisualStyleBackColor = false;
            this.button37.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button37_MouseClick);
            this.button37.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.button37_PreviewKeyDown);
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label77.Location = new System.Drawing.Point(6, 42);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(43, 29);
            this.label77.TabIndex = 10;
            this.label77.Text = "На";
            // 
            // tabPage12
            // 
            this.tabPage12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tabPage12.Controls.Add(this.dataGridView9);
            this.tabPage12.Controls.Add(this.panel25);
            this.tabPage12.Location = new System.Drawing.Point(4, 22);
            this.tabPage12.Name = "tabPage12";
            this.tabPage12.Size = new System.Drawing.Size(881, 543);
            this.tabPage12.TabIndex = 13;
            this.tabPage12.Text = "Печать мероприятия";
            // 
            // dataGridView9
            // 
            this.dataGridView9.AllowUserToAddRows = false;
            this.dataGridView9.AllowUserToDeleteRows = false;
            this.dataGridView9.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView9.Location = new System.Drawing.Point(0, 98);
            this.dataGridView9.MultiSelect = false;
            this.dataGridView9.Name = "dataGridView9";
            this.dataGridView9.ReadOnly = true;
            this.dataGridView9.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView9.Size = new System.Drawing.Size(881, 445);
            this.dataGridView9.TabIndex = 15;
            this.dataGridView9.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView9_KeyDown);
            // 
            // panel25
            // 
            this.panel25.Controls.Add(this.button38);
            this.panel25.Controls.Add(this.textBox35);
            this.panel25.Controls.Add(this.label79);
            this.panel25.Controls.Add(this.label80);
            this.panel25.Location = new System.Drawing.Point(123, 3);
            this.panel25.Name = "panel25";
            this.panel25.Size = new System.Drawing.Size(726, 93);
            this.panel25.TabIndex = 18;
            // 
            // button38
            // 
            this.button38.BackColor = System.Drawing.Color.White;
            this.button38.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button38.Location = new System.Drawing.Point(596, 60);
            this.button38.Name = "button38";
            this.button38.Size = new System.Drawing.Size(127, 30);
            this.button38.TabIndex = 16;
            this.button38.Text = "Печать";
            this.button38.UseVisualStyleBackColor = false;
            this.button38.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button38_MouseClick);
            this.button38.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.button38_PreviewKeyDown);
            // 
            // textBox35
            // 
            this.textBox35.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox35.Location = new System.Drawing.Point(140, 27);
            this.textBox35.Name = "textBox35";
            this.textBox35.Size = new System.Drawing.Size(304, 32);
            this.textBox35.TabIndex = 17;
            this.textBox35.TextChanged += new System.EventHandler(this.textBox35_TextChanged_1);
            this.textBox35.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox35_KeyDown);
            // 
            // label79
            // 
            this.label79.AutoSize = true;
            this.label79.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label79.Location = new System.Drawing.Point(207, 61);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(165, 29);
            this.label79.TabIndex = 14;
            this.label79.Text = "На 26.11.2024";
            // 
            // label80
            // 
            this.label80.AutoSize = true;
            this.label80.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label80.Location = new System.Drawing.Point(32, -5);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(575, 29);
            this.label80.TabIndex = 13;
            this.label80.Text = "Состояние выполнения мероприятия (приказа)";
            // 
            // tabPage13
            // 
            this.tabPage13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tabPage13.Controls.Add(this.dataGridView10);
            this.tabPage13.Controls.Add(this.panel26);
            this.tabPage13.Location = new System.Drawing.Point(4, 22);
            this.tabPage13.Name = "tabPage13";
            this.tabPage13.Size = new System.Drawing.Size(881, 543);
            this.tabPage13.TabIndex = 14;
            this.tabPage13.Text = "Печать показатели";
            // 
            // dataGridView10
            // 
            this.dataGridView10.AllowUserToAddRows = false;
            this.dataGridView10.AllowUserToDeleteRows = false;
            this.dataGridView10.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView10.Location = new System.Drawing.Point(0, 80);
            this.dataGridView10.MultiSelect = false;
            this.dataGridView10.Name = "dataGridView10";
            this.dataGridView10.ReadOnly = true;
            this.dataGridView10.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView10.Size = new System.Drawing.Size(881, 459);
            this.dataGridView10.TabIndex = 36;
            this.dataGridView10.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView10_KeyDown);
            // 
            // panel26
            // 
            this.panel26.Controls.Add(this.button40);
            this.panel26.Controls.Add(this.label83);
            this.panel26.Controls.Add(this.label81);
            this.panel26.Controls.Add(this.dateTimePicker12);
            this.panel26.Controls.Add(this.dateTimePicker13);
            this.panel26.Controls.Add(this.label82);
            this.panel26.Location = new System.Drawing.Point(93, 3);
            this.panel26.Name = "panel26";
            this.panel26.Size = new System.Drawing.Size(772, 76);
            this.panel26.TabIndex = 40;
            // 
            // button40
            // 
            this.button40.BackColor = System.Drawing.Color.White;
            this.button40.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button40.Location = new System.Drawing.Point(637, 43);
            this.button40.Name = "button40";
            this.button40.Size = new System.Drawing.Size(127, 30);
            this.button40.TabIndex = 37;
            this.button40.Text = "Печать";
            this.button40.UseVisualStyleBackColor = false;
            this.button40.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button40_MouseClick);
            this.button40.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.button40_PreviewKeyDown);
            // 
            // label83
            // 
            this.label83.AutoSize = true;
            this.label83.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label83.Location = new System.Drawing.Point(127, 39);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(30, 29);
            this.label83.TabIndex = 39;
            this.label83.Text = "С";
            // 
            // label81
            // 
            this.label81.AutoSize = true;
            this.label81.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label81.Location = new System.Drawing.Point(318, 38);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(44, 29);
            this.label81.TabIndex = 35;
            this.label81.Text = "По";
            // 
            // dateTimePicker12
            // 
            this.dateTimePicker12.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePicker12.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker12.Location = new System.Drawing.Point(163, 36);
            this.dateTimePicker12.Name = "dateTimePicker12";
            this.dateTimePicker12.Size = new System.Drawing.Size(149, 32);
            this.dateTimePicker12.TabIndex = 34;
            this.dateTimePicker12.ValueChanged += new System.EventHandler(this.dateTimePicker12_ValueChanged);
            this.dateTimePicker12.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dateTimePicker12_KeyDown);
            // 
            // dateTimePicker13
            // 
            this.dateTimePicker13.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePicker13.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker13.Location = new System.Drawing.Point(368, 36);
            this.dateTimePicker13.Name = "dateTimePicker13";
            this.dateTimePicker13.Size = new System.Drawing.Size(149, 32);
            this.dateTimePicker13.TabIndex = 33;
            this.dateTimePicker13.ValueChanged += new System.EventHandler(this.dateTimePicker13_ValueChanged);
            this.dateTimePicker13.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dateTimePicker13_KeyDown);
            // 
            // label82
            // 
            this.label82.AutoSize = true;
            this.label82.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label82.Location = new System.Drawing.Point(0, 0);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(677, 29);
            this.label82.TabIndex = 32;
            this.label82.Text = "Текущие значения показателей работы подразделений ";
            // 
            // tabPage14
            // 
            this.tabPage14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tabPage14.Controls.Add(this.dataGridView13);
            this.tabPage14.Controls.Add(this.dataGridView11);
            this.tabPage14.Controls.Add(this.panel27);
            this.tabPage14.Controls.Add(this.panel28);
            this.tabPage14.Location = new System.Drawing.Point(4, 22);
            this.tabPage14.Name = "tabPage14";
            this.tabPage14.Size = new System.Drawing.Size(881, 543);
            this.tabPage14.TabIndex = 15;
            this.tabPage14.Text = "Печать кооф";
            // 
            // dataGridView13
            // 
            this.dataGridView13.AllowUserToAddRows = false;
            this.dataGridView13.AllowUserToDeleteRows = false;
            this.dataGridView13.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView13.Location = new System.Drawing.Point(0, 423);
            this.dataGridView13.MultiSelect = false;
            this.dataGridView13.Name = "dataGridView13";
            this.dataGridView13.ReadOnly = true;
            this.dataGridView13.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView13.Size = new System.Drawing.Size(881, 120);
            this.dataGridView13.TabIndex = 52;
            this.dataGridView13.Visible = false;
            this.dataGridView13.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView13_KeyDown);
            // 
            // dataGridView11
            // 
            this.dataGridView11.AllowUserToAddRows = false;
            this.dataGridView11.AllowUserToDeleteRows = false;
            this.dataGridView11.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView11.Location = new System.Drawing.Point(0, 112);
            this.dataGridView11.MultiSelect = false;
            this.dataGridView11.Name = "dataGridView11";
            this.dataGridView11.ReadOnly = true;
            this.dataGridView11.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView11.Size = new System.Drawing.Size(881, 247);
            this.dataGridView11.TabIndex = 44;
            this.dataGridView11.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView11_KeyDown);
            // 
            // panel27
            // 
            this.panel27.Controls.Add(this.textBox36);
            this.panel27.Controls.Add(this.button43);
            this.panel27.Controls.Add(this.textBox37);
            this.panel27.Controls.Add(this.label87);
            this.panel27.Controls.Add(this.label86);
            this.panel27.Controls.Add(this.label84);
            this.panel27.Controls.Add(this.button42);
            this.panel27.Controls.Add(this.label85);
            this.panel27.Controls.Add(this.dateTimePicker14);
            this.panel27.Controls.Add(this.dateTimePicker15);
            this.panel27.Location = new System.Drawing.Point(111, 3);
            this.panel27.Name = "panel27";
            this.panel27.Size = new System.Drawing.Size(738, 109);
            this.panel27.TabIndex = 55;
            // 
            // textBox36
            // 
            this.textBox36.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox36.Location = new System.Drawing.Point(209, 33);
            this.textBox36.Name = "textBox36";
            this.textBox36.Size = new System.Drawing.Size(95, 32);
            this.textBox36.TabIndex = 51;
            this.textBox36.TextChanged += new System.EventHandler(this.textBox36_TextChanged);
            this.textBox36.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox36_KeyDown);
            // 
            // button43
            // 
            this.button43.BackColor = System.Drawing.Color.White;
            this.button43.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button43.Location = new System.Drawing.Point(306, 33);
            this.button43.Name = "button43";
            this.button43.Size = new System.Drawing.Size(21, 29);
            this.button43.TabIndex = 50;
            this.button43.Text = "^";
            this.button43.UseVisualStyleBackColor = false;
            this.button43.Click += new System.EventHandler(this.button43_Click);
            // 
            // textBox37
            // 
            this.textBox37.Enabled = false;
            this.textBox37.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox37.Location = new System.Drawing.Point(337, 33);
            this.textBox37.Name = "textBox37";
            this.textBox37.Size = new System.Drawing.Size(251, 32);
            this.textBox37.TabIndex = 49;
            // 
            // label87
            // 
            this.label87.AutoSize = true;
            this.label87.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label87.Location = new System.Drawing.Point(32, 33);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(171, 29);
            this.label87.TabIndex = 48;
            this.label87.Text = "Исполнитель";
            // 
            // label86
            // 
            this.label86.AutoSize = true;
            this.label86.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label86.Location = new System.Drawing.Point(41, 0);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(594, 29);
            this.label86.TabIndex = 40;
            this.label86.Text = "Справка о составляющих коэффициента качества";
            // 
            // label84
            // 
            this.label84.AutoSize = true;
            this.label84.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label84.Location = new System.Drawing.Point(104, 72);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(30, 29);
            this.label84.TabIndex = 47;
            this.label84.Text = "С";
            // 
            // button42
            // 
            this.button42.BackColor = System.Drawing.Color.White;
            this.button42.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button42.Location = new System.Drawing.Point(611, 76);
            this.button42.Name = "button42";
            this.button42.Size = new System.Drawing.Size(127, 30);
            this.button42.TabIndex = 45;
            this.button42.Text = "Печать";
            this.button42.UseVisualStyleBackColor = false;
            this.button42.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button42_MouseClick);
            this.button42.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.button42_PreviewKeyDown);
            // 
            // label85
            // 
            this.label85.AutoSize = true;
            this.label85.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label85.Location = new System.Drawing.Point(295, 71);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(44, 29);
            this.label85.TabIndex = 43;
            this.label85.Text = "По";
            // 
            // dateTimePicker14
            // 
            this.dateTimePicker14.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePicker14.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker14.Location = new System.Drawing.Point(140, 69);
            this.dateTimePicker14.Name = "dateTimePicker14";
            this.dateTimePicker14.Size = new System.Drawing.Size(149, 32);
            this.dateTimePicker14.TabIndex = 42;
            this.dateTimePicker14.ValueChanged += new System.EventHandler(this.dateTimePicker14_ValueChanged);
            this.dateTimePicker14.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dateTimePicker14_KeyDown);
            // 
            // dateTimePicker15
            // 
            this.dateTimePicker15.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePicker15.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker15.Location = new System.Drawing.Point(345, 69);
            this.dateTimePicker15.Name = "dateTimePicker15";
            this.dateTimePicker15.Size = new System.Drawing.Size(149, 32);
            this.dateTimePicker15.TabIndex = 41;
            this.dateTimePicker15.ValueChanged += new System.EventHandler(this.dateTimePicker15_ValueChanged);
            this.dateTimePicker15.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dateTimePicker15_KeyDown);
            // 
            // panel28
            // 
            this.panel28.Controls.Add(this.label100);
            this.panel28.Controls.Add(this.label99);
            this.panel28.Location = new System.Drawing.Point(125, 357);
            this.panel28.Name = "panel28";
            this.panel28.Size = new System.Drawing.Size(675, 60);
            this.panel28.TabIndex = 56;
            // 
            // label100
            // 
            this.label100.AutoSize = true;
            this.label100.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label100.Location = new System.Drawing.Point(8, 29);
            this.label100.Name = "label100";
            this.label100.Size = new System.Drawing.Size(667, 29);
            this.label100.TabIndex = 54;
            this.label100.Text = "Ваша работа учтена УАСКИ следующими показателями";
            this.label100.Visible = false;
            // 
            // label99
            // 
            this.label99.AutoSize = true;
            this.label99.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label99.Location = new System.Drawing.Point(142, 0);
            this.label99.Name = "label99";
            this.label99.Size = new System.Drawing.Size(383, 29);
            this.label99.TabIndex = 53;
            this.label99.Text = "Уважаемый товарищ Месси Л.О";
            this.label99.Visible = false;
            // 
            // tabPage15
            // 
            this.tabPage15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tabPage15.Controls.Add(this.panel20);
            this.tabPage15.Controls.Add(this.panel10);
            this.tabPage15.Location = new System.Drawing.Point(4, 22);
            this.tabPage15.Name = "tabPage15";
            this.tabPage15.Size = new System.Drawing.Size(881, 543);
            this.tabPage15.TabIndex = 16;
            this.tabPage15.Text = "Претензии";
            // 
            // panel20
            // 
            this.panel20.BackColor = System.Drawing.Color.Silver;
            this.panel20.Controls.Add(this.button44);
            this.panel20.Controls.Add(this.button45);
            this.panel20.Controls.Add(this.button46);
            this.panel20.Location = new System.Drawing.Point(629, 58);
            this.panel20.Name = "panel20";
            this.panel20.Size = new System.Drawing.Size(204, 208);
            this.panel20.TabIndex = 36;
            // 
            // button44
            // 
            this.button44.BackColor = System.Drawing.Color.White;
            this.button44.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button44.Location = new System.Drawing.Point(22, 151);
            this.button44.Name = "button44";
            this.button44.Size = new System.Drawing.Size(164, 43);
            this.button44.TabIndex = 2;
            this.button44.Text = "Удалить";
            this.button44.UseVisualStyleBackColor = false;
            this.button44.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button44_MouseClick);
            this.button44.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.button44_PreviewKeyDown);
            // 
            // button45
            // 
            this.button45.BackColor = System.Drawing.Color.White;
            this.button45.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button45.Location = new System.Drawing.Point(22, 79);
            this.button45.Name = "button45";
            this.button45.Size = new System.Drawing.Size(164, 43);
            this.button45.TabIndex = 1;
            this.button45.Text = "Изменить";
            this.button45.UseVisualStyleBackColor = false;
            this.button45.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button45_MouseClick);
            this.button45.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.button45_PreviewKeyDown);
            // 
            // button46
            // 
            this.button46.BackColor = System.Drawing.Color.White;
            this.button46.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button46.Location = new System.Drawing.Point(22, 11);
            this.button46.Name = "button46";
            this.button46.Size = new System.Drawing.Size(164, 43);
            this.button46.TabIndex = 0;
            this.button46.Text = "Создать";
            this.button46.UseVisualStyleBackColor = false;
            this.button46.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button46_MouseClick);
            this.button46.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.button46_PreviewKeyDown);
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.Silver;
            this.panel10.Controls.Add(this.label94);
            this.panel10.Controls.Add(this.label93);
            this.panel10.Controls.Add(this.label92);
            this.panel10.Controls.Add(this.label91);
            this.panel10.Controls.Add(this.textBox39);
            this.panel10.Controls.Add(this.dateTimePicker16);
            this.panel10.Controls.Add(this.label90);
            this.panel10.Controls.Add(this.label89);
            this.panel10.Controls.Add(this.textBox38);
            this.panel10.Controls.Add(this.label102);
            this.panel10.Location = new System.Drawing.Point(7, 13);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(603, 316);
            this.panel10.TabIndex = 2;
            // 
            // label94
            // 
            this.label94.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label94.ForeColor = System.Drawing.Color.Red;
            this.label94.Location = new System.Drawing.Point(30, 289);
            this.label94.Name = "label94";
            this.label94.Size = new System.Drawing.Size(238, 13);
            this.label94.TabIndex = 33;
            this.label94.Text = "Ошибка";
            this.label94.Visible = false;
            // 
            // label93
            // 
            this.label93.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label93.ForeColor = System.Drawing.Color.Red;
            this.label93.Location = new System.Drawing.Point(30, 200);
            this.label93.Name = "label93";
            this.label93.Size = new System.Drawing.Size(238, 13);
            this.label93.TabIndex = 32;
            this.label93.Text = "Ошибка";
            this.label93.Visible = false;
            // 
            // label92
            // 
            this.label92.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label92.ForeColor = System.Drawing.Color.Red;
            this.label92.Location = new System.Drawing.Point(30, 117);
            this.label92.Name = "label92";
            this.label92.Size = new System.Drawing.Size(279, 13);
            this.label92.TabIndex = 31;
            this.label92.Text = "Ошибка";
            this.label92.Visible = false;
            // 
            // label91
            // 
            this.label91.AutoSize = true;
            this.label91.Font = new System.Drawing.Font("Microsoft Uighur", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label91.Location = new System.Drawing.Point(28, 227);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(80, 26);
            this.label91.TabIndex = 30;
            this.label91.Text = "Оценка";
            // 
            // textBox39
            // 
            this.textBox39.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox39.Location = new System.Drawing.Point(33, 256);
            this.textBox39.Name = "textBox39";
            this.textBox39.Size = new System.Drawing.Size(119, 30);
            this.textBox39.TabIndex = 29;
            this.textBox39.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox39_KeyDown);
            // 
            // dateTimePicker16
            // 
            this.dateTimePicker16.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePicker16.Location = new System.Drawing.Point(33, 168);
            this.dateTimePicker16.Name = "dateTimePicker16";
            this.dateTimePicker16.Size = new System.Drawing.Size(235, 29);
            this.dateTimePicker16.TabIndex = 28;
            this.dateTimePicker16.ValueChanged += new System.EventHandler(this.dateTimePicker16_ValueChanged);
            this.dateTimePicker16.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dateTimePicker16_KeyDown);
            // 
            // label90
            // 
            this.label90.AutoSize = true;
            this.label90.Font = new System.Drawing.Font("Microsoft Uighur", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label90.Location = new System.Drawing.Point(28, 139);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(184, 26);
            this.label90.TabIndex = 27;
            this.label90.Text = "Дата выставления";
            // 
            // label89
            // 
            this.label89.AutoSize = true;
            this.label89.Font = new System.Drawing.Font("Microsoft Uighur", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label89.Location = new System.Drawing.Point(28, 55);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(48, 26);
            this.label89.TabIndex = 26;
            this.label89.Text = "Код";
            // 
            // textBox38
            // 
            this.textBox38.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox38.Location = new System.Drawing.Point(33, 84);
            this.textBox38.Name = "textBox38";
            this.textBox38.Size = new System.Drawing.Size(276, 30);
            this.textBox38.TabIndex = 25;
            this.textBox38.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox38_KeyDown);
            // 
            // label102
            // 
            this.label102.AutoSize = true;
            this.label102.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label102.Location = new System.Drawing.Point(3, 9);
            this.label102.Name = "label102";
            this.label102.Size = new System.Drawing.Size(212, 24);
            this.label102.TabIndex = 2;
            this.label102.Text = "Претензия на задачу";
            // 
            // tabPage16
            // 
            this.tabPage16.Controls.Add(this.button54);
            this.tabPage16.Controls.Add(this.dataGridView12);
            this.tabPage16.Controls.Add(this.panel21);
            this.tabPage16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabPage16.Location = new System.Drawing.Point(4, 22);
            this.tabPage16.Name = "tabPage16";
            this.tabPage16.Size = new System.Drawing.Size(881, 543);
            this.tabPage16.TabIndex = 17;
            this.tabPage16.Text = "Просм. претензий";
            this.tabPage16.UseVisualStyleBackColor = true;
            // 
            // button54
            // 
            this.button54.BackColor = System.Drawing.Color.White;
            this.button54.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button54.Location = new System.Drawing.Point(2, 0);
            this.button54.Name = "button54";
            this.button54.Size = new System.Drawing.Size(17, 30);
            this.button54.TabIndex = 28;
            this.button54.Text = "<";
            this.button54.UseVisualStyleBackColor = false;
            this.button54.Click += new System.EventHandler(this.button54_Click);
            // 
            // dataGridView12
            // 
            this.dataGridView12.AllowUserToAddRows = false;
            this.dataGridView12.AllowUserToDeleteRows = false;
            this.dataGridView12.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView12.Location = new System.Drawing.Point(245, 0);
            this.dataGridView12.MultiSelect = false;
            this.dataGridView12.Name = "dataGridView12";
            this.dataGridView12.ReadOnly = true;
            this.dataGridView12.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView12.Size = new System.Drawing.Size(636, 543);
            this.dataGridView12.TabIndex = 17;
            this.dataGridView12.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView12_KeyDown);
            // 
            // panel21
            // 
            this.panel21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel21.Controls.Add(this.checkBox6);
            this.panel21.Controls.Add(this.checkBox5);
            this.panel21.Controls.Add(this.button51);
            this.panel21.Controls.Add(this.panel22);
            this.panel21.Controls.Add(this.checkBox4);
            this.panel21.Controls.Add(this.textBox40);
            this.panel21.Controls.Add(this.label98);
            this.panel21.Location = new System.Drawing.Point(-1, 0);
            this.panel21.Name = "panel21";
            this.panel21.Size = new System.Drawing.Size(246, 542);
            this.panel21.TabIndex = 16;
            this.panel21.Visible = false;
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.Location = new System.Drawing.Point(8, 113);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(100, 24);
            this.checkBox6.TabIndex = 33;
            this.checkBox6.Text = "Рецензии";
            this.checkBox6.UseVisualStyleBackColor = true;
            this.checkBox6.CheckedChanged += new System.EventHandler(this.checkBox6_CheckedChanged);
            this.checkBox6.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.checkBox6_PreviewKeyDown);
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(9, 83);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(111, 24);
            this.checkBox5.TabIndex = 32;
            this.checkBox5.Text = "Претензии";
            this.checkBox5.UseVisualStyleBackColor = true;
            this.checkBox5.CheckedChanged += new System.EventHandler(this.checkBox5_CheckedChanged);
            this.checkBox5.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.checkBox5_PreviewKeyDown);
            // 
            // button51
            // 
            this.button51.BackColor = System.Drawing.Color.White;
            this.button51.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button51.Location = new System.Drawing.Point(226, 0);
            this.button51.Name = "button51";
            this.button51.Size = new System.Drawing.Size(17, 30);
            this.button51.TabIndex = 29;
            this.button51.Text = ">";
            this.button51.UseVisualStyleBackColor = false;
            this.button51.Click += new System.EventHandler(this.button51_Click);
            // 
            // panel22
            // 
            this.panel22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel22.Controls.Add(this.button52);
            this.panel22.Controls.Add(this.button53);
            this.panel22.Controls.Add(this.dateTimePicker17);
            this.panel22.Controls.Add(this.dateTimePicker18);
            this.panel22.Controls.Add(this.label95);
            this.panel22.Controls.Add(this.label96);
            this.panel22.Location = new System.Drawing.Point(2, 201);
            this.panel22.Name = "panel22";
            this.panel22.Size = new System.Drawing.Size(241, 129);
            this.panel22.TabIndex = 14;
            // 
            // button52
            // 
            this.button52.BackColor = System.Drawing.Color.White;
            this.button52.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button52.Location = new System.Drawing.Point(222, 79);
            this.button52.Name = "button52";
            this.button52.Size = new System.Drawing.Size(19, 26);
            this.button52.TabIndex = 31;
            this.button52.Text = "^";
            this.button52.UseVisualStyleBackColor = false;
            this.button52.Click += new System.EventHandler(this.button52_Click);
            // 
            // button53
            // 
            this.button53.BackColor = System.Drawing.Color.White;
            this.button53.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button53.Location = new System.Drawing.Point(222, 27);
            this.button53.Name = "button53";
            this.button53.Size = new System.Drawing.Size(19, 26);
            this.button53.TabIndex = 30;
            this.button53.Text = "^";
            this.button53.UseVisualStyleBackColor = false;
            this.button53.Click += new System.EventHandler(this.button53_Click);
            // 
            // dateTimePicker17
            // 
            this.dateTimePicker17.Location = new System.Drawing.Point(5, 27);
            this.dateTimePicker17.Name = "dateTimePicker17";
            this.dateTimePicker17.Size = new System.Drawing.Size(214, 26);
            this.dateTimePicker17.TabIndex = 5;
            this.dateTimePicker17.ValueChanged += new System.EventHandler(this.dateTimePicker17_ValueChanged);
            this.dateTimePicker17.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dateTimePicker17_KeyDown);
            // 
            // dateTimePicker18
            // 
            this.dateTimePicker18.Location = new System.Drawing.Point(5, 79);
            this.dateTimePicker18.Name = "dateTimePicker18";
            this.dateTimePicker18.Size = new System.Drawing.Size(214, 26);
            this.dateTimePicker18.TabIndex = 4;
            this.dateTimePicker18.ValueChanged += new System.EventHandler(this.dateTimePicker18_ValueChanged);
            this.dateTimePicker18.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dateTimePicker18_KeyDown);
            // 
            // label95
            // 
            this.label95.AutoSize = true;
            this.label95.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label95.Location = new System.Drawing.Point(1, 4);
            this.label95.Name = "label95";
            this.label95.Size = new System.Drawing.Size(30, 20);
            this.label95.TabIndex = 6;
            this.label95.Text = "От";
            // 
            // label96
            // 
            this.label96.AutoSize = true;
            this.label96.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label96.Location = new System.Drawing.Point(1, 56);
            this.label96.Name = "label96";
            this.label96.Size = new System.Drawing.Size(30, 20);
            this.label96.TabIndex = 7;
            this.label96.Text = "До";
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.checkBox4.Location = new System.Drawing.Point(6, 171);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(152, 24);
            this.checkBox4.TabIndex = 12;
            this.checkBox4.Text = "Учитывать дату";
            this.checkBox4.UseVisualStyleBackColor = false;
            this.checkBox4.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            this.checkBox4.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.checkBox4_PreviewKeyDown);
            // 
            // textBox40
            // 
            this.textBox40.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox40.Location = new System.Drawing.Point(6, 35);
            this.textBox40.Name = "textBox40";
            this.textBox40.Size = new System.Drawing.Size(235, 26);
            this.textBox40.TabIndex = 10;
            this.textBox40.TextChanged += new System.EventHandler(this.textBox40_TextChanged);
            this.textBox40.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox40_KeyDown);
            // 
            // label98
            // 
            this.label98.AutoSize = true;
            this.label98.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label98.Location = new System.Drawing.Point(3, 12);
            this.label98.Name = "label98";
            this.label98.Size = new System.Drawing.Size(39, 20);
            this.label98.TabIndex = 3;
            this.label98.Text = "Код";
            // 
            // tabPage17
            // 
            this.tabPage17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tabPage17.Controls.Add(this.panel29);
            this.tabPage17.Controls.Add(this.dataGridView14);
            this.tabPage17.Location = new System.Drawing.Point(4, 22);
            this.tabPage17.Name = "tabPage17";
            this.tabPage17.Size = new System.Drawing.Size(881, 543);
            this.tabPage17.TabIndex = 18;
            this.tabPage17.Text = "Печать планов";
            // 
            // panel29
            // 
            this.panel29.Controls.Add(this.textBox42);
            this.panel29.Controls.Add(this.button50);
            this.panel29.Controls.Add(this.button56);
            this.panel29.Controls.Add(this.textBox43);
            this.panel29.Controls.Add(this.label54);
            this.panel29.Controls.Add(this.label69);
            this.panel29.Controls.Add(this.dateTimePicker20);
            this.panel29.Controls.Add(this.dateTimePicker21);
            this.panel29.Controls.Add(this.label70);
            this.panel29.Location = new System.Drawing.Point(66, 3);
            this.panel29.Name = "panel29";
            this.panel29.Size = new System.Drawing.Size(777, 80);
            this.panel29.TabIndex = 36;
            // 
            // textBox42
            // 
            this.textBox42.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox42.Location = new System.Drawing.Point(238, 44);
            this.textBox42.Name = "textBox42";
            this.textBox42.Size = new System.Drawing.Size(95, 32);
            this.textBox42.TabIndex = 33;
            this.textBox42.TextChanged += new System.EventHandler(this.textBox42_TextChanged);
            this.textBox42.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox42_KeyDown);
            // 
            // button50
            // 
            this.button50.BackColor = System.Drawing.Color.White;
            this.button50.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button50.Location = new System.Drawing.Point(335, 44);
            this.button50.Name = "button50";
            this.button50.Size = new System.Drawing.Size(21, 29);
            this.button50.TabIndex = 32;
            this.button50.Text = "^";
            this.button50.UseVisualStyleBackColor = false;
            this.button50.Click += new System.EventHandler(this.button50_Click);
            // 
            // button56
            // 
            this.button56.BackColor = System.Drawing.Color.White;
            this.button56.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button56.Location = new System.Drawing.Point(648, 46);
            this.button56.Name = "button56";
            this.button56.Size = new System.Drawing.Size(127, 30);
            this.button56.TabIndex = 7;
            this.button56.Text = "Печать";
            this.button56.UseVisualStyleBackColor = false;
            this.button56.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.button56_PreviewKeyDown);
            // 
            // textBox43
            // 
            this.textBox43.Enabled = false;
            this.textBox43.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox43.Location = new System.Drawing.Point(366, 44);
            this.textBox43.Name = "textBox43";
            this.textBox43.Size = new System.Drawing.Size(251, 32);
            this.textBox43.TabIndex = 5;
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label54.Location = new System.Drawing.Point(61, 44);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(171, 29);
            this.label54.TabIndex = 4;
            this.label54.Text = "Исполнитель";
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label69.Location = new System.Drawing.Point(418, 7);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(44, 29);
            this.label69.TabIndex = 3;
            this.label69.Text = "По";
            // 
            // dateTimePicker20
            // 
            this.dateTimePicker20.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePicker20.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker20.Location = new System.Drawing.Point(263, 8);
            this.dateTimePicker20.Name = "dateTimePicker20";
            this.dateTimePicker20.Size = new System.Drawing.Size(149, 32);
            this.dateTimePicker20.TabIndex = 2;
            this.dateTimePicker20.ValueChanged += new System.EventHandler(this.dateTimePicker20_ValueChanged);
            this.dateTimePicker20.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dateTimePicker20_KeyDown);
            // 
            // dateTimePicker21
            // 
            this.dateTimePicker21.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePicker21.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker21.Location = new System.Drawing.Point(465, 7);
            this.dateTimePicker21.Name = "dateTimePicker21";
            this.dateTimePicker21.Size = new System.Drawing.Size(149, 32);
            this.dateTimePicker21.TabIndex = 1;
            this.dateTimePicker21.ValueChanged += new System.EventHandler(this.dateTimePicker21_ValueChanged);
            this.dateTimePicker21.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dateTimePicker21_KeyDown);
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label70.Location = new System.Drawing.Point(149, 8);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(108, 29);
            this.label70.TabIndex = 0;
            this.label70.Text = "Планы с";
            // 
            // dataGridView14
            // 
            this.dataGridView14.AllowUserToAddRows = false;
            this.dataGridView14.AllowUserToDeleteRows = false;
            this.dataGridView14.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView14.Location = new System.Drawing.Point(0, 84);
            this.dataGridView14.MultiSelect = false;
            this.dataGridView14.Name = "dataGridView14";
            this.dataGridView14.ReadOnly = true;
            this.dataGridView14.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView14.Size = new System.Drawing.Size(881, 459);
            this.dataGridView14.TabIndex = 35;
            this.dataGridView14.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView14_KeyDown);
            // 
            // tabPage18
            // 
            this.tabPage18.Controls.Add(this.button57);
            this.tabPage18.Controls.Add(this.dataGridView15);
            this.tabPage18.Controls.Add(this.panel30);
            this.tabPage18.Location = new System.Drawing.Point(4, 22);
            this.tabPage18.Name = "tabPage18";
            this.tabPage18.Size = new System.Drawing.Size(881, 543);
            this.tabPage18.TabIndex = 19;
            this.tabPage18.Text = "Просмотр д";
            this.tabPage18.UseVisualStyleBackColor = true;
            // 
            // button57
            // 
            this.button57.BackColor = System.Drawing.Color.White;
            this.button57.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button57.Location = new System.Drawing.Point(4, 1);
            this.button57.Name = "button57";
            this.button57.Size = new System.Drawing.Size(17, 30);
            this.button57.TabIndex = 31;
            this.button57.Text = "<";
            this.button57.UseVisualStyleBackColor = false;
            this.button57.Click += new System.EventHandler(this.button57_Click);
            // 
            // dataGridView15
            // 
            this.dataGridView15.AllowUserToAddRows = false;
            this.dataGridView15.AllowUserToDeleteRows = false;
            this.dataGridView15.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView15.Location = new System.Drawing.Point(244, -2);
            this.dataGridView15.MultiSelect = false;
            this.dataGridView15.Name = "dataGridView15";
            this.dataGridView15.ReadOnly = true;
            this.dataGridView15.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView15.Size = new System.Drawing.Size(636, 547);
            this.dataGridView15.TabIndex = 30;
            this.dataGridView15.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView15_KeyDown);
            // 
            // panel30
            // 
            this.panel30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel30.Controls.Add(this.button59);
            this.panel30.Controls.Add(this.panel31);
            this.panel30.Controls.Add(this.checkBox9);
            this.panel30.Controls.Add(this.textBox44);
            this.panel30.Controls.Add(this.label101);
            this.panel30.Location = new System.Drawing.Point(1, 1);
            this.panel30.Name = "panel30";
            this.panel30.Size = new System.Drawing.Size(246, 542);
            this.panel30.TabIndex = 29;
            this.panel30.Visible = false;
            // 
            // button59
            // 
            this.button59.BackColor = System.Drawing.Color.White;
            this.button59.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button59.Location = new System.Drawing.Point(226, 0);
            this.button59.Name = "button59";
            this.button59.Size = new System.Drawing.Size(17, 30);
            this.button59.TabIndex = 29;
            this.button59.Text = ">";
            this.button59.UseVisualStyleBackColor = false;
            this.button59.Click += new System.EventHandler(this.button59_Click);
            // 
            // panel31
            // 
            this.panel31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel31.Controls.Add(this.button60);
            this.panel31.Controls.Add(this.button61);
            this.panel31.Controls.Add(this.dateTimePicker22);
            this.panel31.Controls.Add(this.dateTimePicker23);
            this.panel31.Controls.Add(this.label88);
            this.panel31.Controls.Add(this.label97);
            this.panel31.Location = new System.Drawing.Point(3, 106);
            this.panel31.Name = "panel31";
            this.panel31.Size = new System.Drawing.Size(241, 129);
            this.panel31.TabIndex = 14;
            // 
            // button60
            // 
            this.button60.BackColor = System.Drawing.Color.White;
            this.button60.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button60.Location = new System.Drawing.Point(222, 79);
            this.button60.Name = "button60";
            this.button60.Size = new System.Drawing.Size(19, 26);
            this.button60.TabIndex = 31;
            this.button60.Text = "^";
            this.button60.UseVisualStyleBackColor = false;
            this.button60.Click += new System.EventHandler(this.button60_Click);
            // 
            // button61
            // 
            this.button61.BackColor = System.Drawing.Color.White;
            this.button61.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button61.Location = new System.Drawing.Point(222, 27);
            this.button61.Name = "button61";
            this.button61.Size = new System.Drawing.Size(19, 26);
            this.button61.TabIndex = 30;
            this.button61.Text = "^";
            this.button61.UseVisualStyleBackColor = false;
            this.button61.Click += new System.EventHandler(this.button61_Click);
            // 
            // dateTimePicker22
            // 
            this.dateTimePicker22.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePicker22.Location = new System.Drawing.Point(5, 27);
            this.dateTimePicker22.Name = "dateTimePicker22";
            this.dateTimePicker22.Size = new System.Drawing.Size(214, 26);
            this.dateTimePicker22.TabIndex = 5;
            this.dateTimePicker22.ValueChanged += new System.EventHandler(this.dateTimePicker22_ValueChanged);
            this.dateTimePicker22.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dateTimePicker22_KeyDown);
            // 
            // dateTimePicker23
            // 
            this.dateTimePicker23.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePicker23.Location = new System.Drawing.Point(5, 79);
            this.dateTimePicker23.Name = "dateTimePicker23";
            this.dateTimePicker23.Size = new System.Drawing.Size(214, 26);
            this.dateTimePicker23.TabIndex = 4;
            this.dateTimePicker23.ValueChanged += new System.EventHandler(this.dateTimePicker23_ValueChanged);
            this.dateTimePicker23.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dateTimePicker23_KeyDown);
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label88.Location = new System.Drawing.Point(1, 4);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(30, 20);
            this.label88.TabIndex = 6;
            this.label88.Text = "От";
            // 
            // label97
            // 
            this.label97.AutoSize = true;
            this.label97.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label97.Location = new System.Drawing.Point(1, 56);
            this.label97.Name = "label97";
            this.label97.Size = new System.Drawing.Size(30, 20);
            this.label97.TabIndex = 7;
            this.label97.Text = "До";
            // 
            // checkBox9
            // 
            this.checkBox9.AutoSize = true;
            this.checkBox9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox9.Location = new System.Drawing.Point(7, 76);
            this.checkBox9.Name = "checkBox9";
            this.checkBox9.Size = new System.Drawing.Size(228, 24);
            this.checkBox9.TabIndex = 12;
            this.checkBox9.Text = "Учитывать дату закрытия";
            this.checkBox9.UseVisualStyleBackColor = true;
            this.checkBox9.CheckedChanged += new System.EventHandler(this.checkBox9_CheckedChanged);
            this.checkBox9.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.checkBox9_PreviewKeyDown);
            // 
            // textBox44
            // 
            this.textBox44.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox44.Location = new System.Drawing.Point(6, 35);
            this.textBox44.Name = "textBox44";
            this.textBox44.Size = new System.Drawing.Size(235, 26);
            this.textBox44.TabIndex = 10;
            this.textBox44.TextChanged += new System.EventHandler(this.textBox44_TextChanged);
            this.textBox44.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox44_KeyDown);
            // 
            // label101
            // 
            this.label101.AutoSize = true;
            this.label101.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label101.Location = new System.Drawing.Point(3, 12);
            this.label101.Name = "label101";
            this.label101.Size = new System.Drawing.Size(39, 20);
            this.label101.TabIndex = 3;
            this.label101.Text = "Код";
            // 
            // panel3
            // 
            this.panel3.Location = new System.Drawing.Point(446, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(441, 144);
            this.panel3.TabIndex = 3;
            this.panel3.Click += new System.EventHandler(this.panel3_Click);
            // 
            // panel4
            // 
            this.panel4.Location = new System.Drawing.Point(2, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(435, 143);
            this.panel4.TabIndex = 4;
            this.panel4.Click += new System.EventHandler(this.panel4_Click);
            // 
            // textBox41
            // 
            this.textBox41.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.textBox41.Font = new System.Drawing.Font("Segoe UI Symbol", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox41.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBox41.Location = new System.Drawing.Point(893, 1);
            this.textBox41.Multiline = true;
            this.textBox41.Name = "textBox41";
            this.textBox41.ReadOnly = true;
            this.textBox41.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox41.Size = new System.Drawing.Size(315, 721);
            this.textBox41.TabIndex = 7;
            this.textBox41.Text = "\r\n";
            // 
            // TimeTimer
            // 
            this.TimeTimer.Interval = 1000;
            this.TimeTimer.Tick += new System.EventHandler(this.TimeTimer_Tick);
            // 
            // Gl_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1207, 722);
            this.Controls.Add(this.textBox41);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.Menu_Step2);
            this.Controls.Add(this.Menu_Step1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Gl_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " УАСКИ 2.4";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Gl_Form_KeyDown);
            this.Resize += new System.EventHandler(this.Gl_Form_Resize);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.panel12.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ispDataGridView)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.panel13.ResumeLayout(false);
            this.panel13.PerformLayout();
            this.panel16.ResumeLayout(false);
            this.panel16.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.tabPage9.ResumeLayout(false);
            this.panel14.ResumeLayout(false);
            this.panel14.PerformLayout();
            this.panel15.ResumeLayout(false);
            this.panel15.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView5)).EndInit();
            this.tabPage10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView6)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel17.ResumeLayout(false);
            this.panel17.PerformLayout();
            this.panel18.ResumeLayout(false);
            this.panel18.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.tabPage7.ResumeLayout(false);
            this.tabPage7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.IspCon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).EndInit();
            this.panel8.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.Tasks.ResumeLayout(false);
            this.panel19.ResumeLayout(false);
            this.panel19.PerformLayout();
            this.panel11.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.tabPage8.ResumeLayout(false);
            this.panel23.ResumeLayout(false);
            this.panel23.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView7)).EndInit();
            this.tabPage11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView8)).EndInit();
            this.panel24.ResumeLayout(false);
            this.panel24.PerformLayout();
            this.tabPage12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView9)).EndInit();
            this.panel25.ResumeLayout(false);
            this.panel25.PerformLayout();
            this.tabPage13.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView10)).EndInit();
            this.panel26.ResumeLayout(false);
            this.panel26.PerformLayout();
            this.tabPage14.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView11)).EndInit();
            this.panel27.ResumeLayout(false);
            this.panel27.PerformLayout();
            this.panel28.ResumeLayout(false);
            this.panel28.PerformLayout();
            this.tabPage15.ResumeLayout(false);
            this.panel20.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.tabPage16.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView12)).EndInit();
            this.panel21.ResumeLayout(false);
            this.panel21.PerformLayout();
            this.panel22.ResumeLayout(false);
            this.panel22.PerformLayout();
            this.tabPage17.ResumeLayout(false);
            this.panel29.ResumeLayout(false);
            this.panel29.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView14)).EndInit();
            this.tabPage18.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView15)).EndInit();
            this.panel30.ResumeLayout(false);
            this.panel30.PerformLayout();
            this.panel31.ResumeLayout(false);
            this.panel31.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.ListBox Menu_Step1;
        public System.Windows.Forms.ListBox Menu_Step2;
        public System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.TextBox textBox7;
        public System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox textBox4;
        public System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.Button button1;
        public System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel panel6;
        public System.Windows.Forms.TextBox textBox8;
        public System.Windows.Forms.TextBox textBox10;
        public System.Windows.Forms.TextBox textBox9;
        public System.Windows.Forms.TextBox textBox11;
        public System.Windows.Forms.TextBox textBox12;
        public System.Windows.Forms.Button button4;
        public System.Windows.Forms.Button button5;
        public System.Windows.Forms.MonthCalendar monthCalendar1;
        public System.Windows.Forms.Label label17;
        public System.Windows.Forms.Label label22;
        public System.Windows.Forms.Label label21;
        public System.Windows.Forms.Label label20;
        public System.Windows.Forms.Label label19;
        public System.Windows.Forms.Label label18;
        public System.Windows.Forms.Label label26;
        public System.Windows.Forms.Label label25;
        public System.Windows.Forms.Label label24;
        public System.Windows.Forms.Label label23;
        public System.Windows.Forms.Label label27;
        private System.Windows.Forms.Panel panel7;
        public System.Windows.Forms.Label label28;
        public System.Windows.Forms.Label label29;
        public System.Windows.Forms.Label label30;
        public System.Windows.Forms.Label label31;
        public System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        public System.Windows.Forms.TextBox textBox14;
        private System.Windows.Forms.Label label34;
        public System.Windows.Forms.TextBox textBox15;
        private System.Windows.Forms.Label label35;
        public System.Windows.Forms.TextBox textBox16;
        private System.Windows.Forms.Label label36;
        public System.Windows.Forms.TextBox textBox17;
        private System.Windows.Forms.Label label37;
        public System.Windows.Forms.TextBox textBox18;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Panel panel8;
        public System.Windows.Forms.Button button7;
        public System.Windows.Forms.Button button6;
        public System.Windows.Forms.TabPage tabPage9;
        public System.Windows.Forms.TabPage tabPage10;
        public System.Windows.Forms.TabPage tabPage1;
        public System.Windows.Forms.TabPage tabPage2;
        public System.Windows.Forms.TabPage tabPage3;
        public System.Windows.Forms.TabPage tabPage4;
        public System.Windows.Forms.TabPage tabPage5;
        public System.Windows.Forms.TabPage tabPage6;
        public System.Windows.Forms.TabPage tabPage7;
        public System.Windows.Forms.TabPage IspCon;
        private System.Windows.Forms.TabPage Tasks;
        public System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Label label43;
        public System.Windows.Forms.TextBox textBox21;
        private System.Windows.Forms.Label label44;
        public System.Windows.Forms.TextBox textBox22;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label46;
        public System.Windows.Forms.TextBox textBox23;
        private System.Windows.Forms.Label label47;
        public System.Windows.Forms.TextBox textBox24;
        private System.Windows.Forms.Label label48;
        public System.Windows.Forms.TextBox textBox25;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label label50;
        public System.Windows.Forms.TextBox textBox26;
        public System.Windows.Forms.DateTimePicker dateTimePicker4;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label42;
        public System.Windows.Forms.TextBox textBox27;
        private System.Windows.Forms.Panel panel11;
        public System.Windows.Forms.Button button10;
        public System.Windows.Forms.Button button11;
        public System.Windows.Forms.Button button12;
        public System.Windows.Forms.Label label56;
        public System.Windows.Forms.Label label55;
        public System.Windows.Forms.Label label53;
        public System.Windows.Forms.Label label51;
        public System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Label label58;
        public System.Windows.Forms.TextBox textBox13;
        public System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Label label59;
        public System.Windows.Forms.TextBox textBox19;
        public System.Windows.Forms.TextBox textBox29;
        public System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Label label39;
        public System.Windows.Forms.TextBox textBox31;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.Label label65;
        public System.Windows.Forms.DateTimePicker dateTimePicker2;
        public System.Windows.Forms.DateTimePicker dateTimePicker3;
        private System.Windows.Forms.Label label66;
        public System.Windows.Forms.TextBox textBox32;
        public System.Windows.Forms.CheckBox checkBox1;
        public System.Windows.Forms.Panel panel15;
        public System.Windows.Forms.Panel panel16;
        public System.Windows.Forms.DateTimePicker dateTimePicker5;
        public System.Windows.Forms.DateTimePicker dateTimePicker6;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.Label label61;
        public System.Windows.Forms.CheckBox checkBox2;
        public System.Windows.Forms.Panel panel17;
        public System.Windows.Forms.CheckBox checkBox3;
        public System.Windows.Forms.TextBox textBox33;
        public System.Windows.Forms.TextBox textBox34;
        private System.Windows.Forms.Label label71;
        public System.Windows.Forms.Panel panel18;
        public System.Windows.Forms.DateTimePicker dateTimePicker7;
        public System.Windows.Forms.DateTimePicker dateTimePicker8;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.Label label68;
        public System.Windows.Forms.Label label72;
        public System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.Button button17;
        public System.Windows.Forms.Button button16;
        private System.Windows.Forms.Button button18;
        public System.Windows.Forms.Button button19;
        public System.Windows.Forms.Button button20;
        public System.Windows.Forms.Button button21;
        public System.Windows.Forms.Button button22;
        public System.Windows.Forms.Button button23;
        private System.Windows.Forms.Button button24;
        private System.Windows.Forms.Button button25;
        private System.Windows.Forms.Button button26;
        public System.Windows.Forms.DateTimePicker dateTimePicker9;
        public System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label40;
        public System.Windows.Forms.Panel panel19;
        private System.Windows.Forms.Button button27;
        public System.Windows.Forms.Label label57;
        public System.Windows.Forms.TextBox textBox28;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Button button29;
        private System.Windows.Forms.Button button28;
        private System.Windows.Forms.Button button31;
        private System.Windows.Forms.Button button30;
        private System.Windows.Forms.Button button33;
        private System.Windows.Forms.Button button32;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.Label label74;
        private System.Windows.Forms.Label label75;
        private System.Windows.Forms.Label label76;
        private System.Windows.Forms.Button button36;
        public System.Windows.Forms.DateTimePicker dateTimePicker11;
        public System.Windows.Forms.DateTimePicker dateTimePicker10;
        public System.Windows.Forms.TextBox textBox20;
        public System.Windows.Forms.Button button34;
        public System.Windows.Forms.TextBox textBox30;
        private System.Windows.Forms.TabPage tabPage11;
        public System.Windows.Forms.Button button37;
        private System.Windows.Forms.Label label78;
        public System.Windows.Forms.Label label77;
        private System.Windows.Forms.TabPage tabPage12;
        public System.Windows.Forms.TextBox textBox35;
        public System.Windows.Forms.Button button38;
        public System.Windows.Forms.Label label79;
        private System.Windows.Forms.Label label80;
        private System.Windows.Forms.TabPage tabPage13;
        public System.Windows.Forms.Button button40;
        private System.Windows.Forms.Label label81;
        public System.Windows.Forms.DateTimePicker dateTimePicker12;
        public System.Windows.Forms.DateTimePicker dateTimePicker13;
        private System.Windows.Forms.Label label82;
        private System.Windows.Forms.Label label83;
        private System.Windows.Forms.TabPage tabPage14;
        private System.Windows.Forms.Label label84;
        public System.Windows.Forms.Button button42;
        private System.Windows.Forms.Label label85;
        public System.Windows.Forms.DateTimePicker dateTimePicker14;
        public System.Windows.Forms.DateTimePicker dateTimePicker15;
        private System.Windows.Forms.Label label86;
        public System.Windows.Forms.TextBox textBox36;
        private System.Windows.Forms.Button button43;
        public System.Windows.Forms.TextBox textBox37;
        private System.Windows.Forms.Label label87;
        private System.Windows.Forms.TabPage tabPage15;
        public System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label label89;
        public System.Windows.Forms.TextBox textBox38;
        public System.Windows.Forms.DateTimePicker dateTimePicker16;
        private System.Windows.Forms.Label label90;
        private System.Windows.Forms.Label label91;
        public System.Windows.Forms.TextBox textBox39;
        private System.Windows.Forms.Panel panel20;
        public System.Windows.Forms.Button button44;
        public System.Windows.Forms.Button button45;
        public System.Windows.Forms.Button button46;
        public System.Windows.Forms.Label label93;
        public System.Windows.Forms.Label label92;
        public System.Windows.Forms.Label label94;
        public System.Windows.Forms.Label label102;
        public System.Windows.Forms.Button button48;
        public System.Windows.Forms.Button button47;
        private System.Windows.Forms.TabPage tabPage16;
        public System.Windows.Forms.Panel panel21;
        public System.Windows.Forms.Button button51;
        public System.Windows.Forms.Panel panel22;
        public System.Windows.Forms.DateTimePicker dateTimePicker17;
        public System.Windows.Forms.DateTimePicker dateTimePicker18;
        private System.Windows.Forms.Label label95;
        private System.Windows.Forms.Label label96;
        public System.Windows.Forms.CheckBox checkBox4;
        public System.Windows.Forms.TextBox textBox40;
        private System.Windows.Forms.Label label98;
        public System.Windows.Forms.Button button54;
        public System.Windows.Forms.CheckBox checkBox6;
        public System.Windows.Forms.CheckBox checkBox5;
        public System.Windows.Forms.Label label99;
        public System.Windows.Forms.Label label100;
        private System.Windows.Forms.DataGridView ispDataGridView;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.DataGridView dataGridView4;
        private System.Windows.Forms.DataGridView dataGridView5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView6;
        private System.Windows.Forms.DataGridView dataGridView7;
        private System.Windows.Forms.DataGridView dataGridView8;
        private System.Windows.Forms.DataGridView dataGridView9;
        private System.Windows.Forms.DataGridView dataGridView10;
        private System.Windows.Forms.DataGridView dataGridView11;
        private System.Windows.Forms.DataGridView dataGridView12;
        private System.Windows.Forms.DataGridView dataGridView13;
        public System.Windows.Forms.TextBox textBox41;
        private System.Windows.Forms.Timer TimeTimer;
        private System.Windows.Forms.MonthCalendar monthCalendar2;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.DateTimePicker dateTimePicker19;
        private System.Windows.Forms.Panel panel23;
        private System.Windows.Forms.Panel panel24;
        private System.Windows.Forms.Panel panel25;
        private System.Windows.Forms.Panel panel26;
        private System.Windows.Forms.Panel panel27;
        private System.Windows.Forms.Panel panel28;
        private TabPage tabPage17;
        private Panel panel29;
        public TextBox textBox42;
        private Button button50;
        public Button button56;
        public TextBox textBox43;
        private Label label54;
        private Label label69;
        public DateTimePicker dateTimePicker20;
        public DateTimePicker dateTimePicker21;
        private Label label70;
        private DataGridView dataGridView14;
        public CheckBox checkBox7;
        public CheckBox checkBox8;
        private TabPage tabPage18;
        public Button button57;
        private DataGridView dataGridView15;
        public Panel panel30;
        public Button button59;
        public Panel panel31;
        private Button button60;
        private Button button61;
        public DateTimePicker dateTimePicker22;
        public DateTimePicker dateTimePicker23;
        private Label label88;
        private Label label97;
        public CheckBox checkBox9;
        public TextBox textBox44;
        private Label label101;
        private Button button52;
        private Button button53;
    }
}

