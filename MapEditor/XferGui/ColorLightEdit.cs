/*
 * MapEditor
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 20.11.2014
 */
using System;
using System.Drawing;

using System.Windows.Forms;
using NoxShared.ObjDataXfer;
using System.Resources;
namespace MapEditor.XferGui
{
    /// <summary>
    /// Description of ColorLightEdit.
    /// </summary>
    public partial class ColorLightEdit : XferEditor
    {
        private Color UnknownRcol;

        private InvisibleLightXfer xfer;
        int lastPulseVal = 30;
        public ColorLightEdit()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
        }

        private void sizable()
        {
            sequenceOptions.Height = 60;

            if (button2.Visible)
                sequenceOptions.Height = 90;

            if (button6.Visible)
                sequenceOptions.Height = 125;



        }




        public override void SetObject(NoxShared.Map.Object obj)
        {

            this.obj = obj;
            xfer = obj.GetExtraData<InvisibleLightXfer>();
            numericUpDown1.Value = xfer.LightIntensity;
            numericUpDown2.Value = xfer.PulseSpeed;
            numericUpDown3.Value = xfer.ChangeIntensity;
            outterSize.Value = xfer.LightRadius;
            shadow.Checked = xfer.type == 1 ? true : false;
            sequenceOptions.Height = 60;


            numericUpDown2.Value = xfer.PulseSpeed;

            xxx.Value = xfer.Unknown10;
            ChangeIntensitySingleNum.Value = xfer.ChangeIntensitySingle;
            PulseSpeedSingleNum.Value = xfer.PulseSpeedSingle;
            unkwn11.Value = xfer.Unknown11;
            /*
            if (xfer.PulseSpeed > 0)
            {
                StaticOptions.Enabled = false;
                PulsingOptions.Enabled = true;
            }
            */
            Color newColor = Color.FromArgb(xfer.R, xfer.G, xfer.B);
            Color newColor2 = Color.FromArgb(xfer.R2, xfer.G2, xfer.B2);

            UnknownRcol = Color.FromArgb(xfer.UnknownR, xfer.UnknownG, xfer.UnknownB);



            /*
            if (xfer.Color2.R == 0 && xfer.Color2.G == 0 && xfer.Color2.B == 0)
                Color2Pulse = Color.White;
            else
                Color2Pulse = xfer.Color2;

            
            if (xfer.UnknownR == 0 && xfer.UnknownG == 0 && xfer.UnknownB == 0)
                UnknownRcol = Color.White;

            */
            Color Color1Pulse = xfer.Color1;
            Color Color2Pulse = xfer.Color2;
            Color Color3Pulse = xfer.Color3;
            Color Color4Pulse = xfer.Color4;
            Color Color5Pulse = xfer.Color5;
            Color Color6Pulse = xfer.Color6;
            Color Color7Pulse = xfer.Color7;
            Color Color8Pulse = xfer.Color8;
            Color Color9Pulse = xfer.Color9;
            Color Color10Pulse = xfer.Color10;





            newColor2 = Color.FromArgb((byte)~newColor2.R, (byte)~newColor2.G, (byte)~newColor2.B);
            if (shadow.Checked)
            {


                newColor = Color.FromArgb((byte)~newColor.R, (byte)~newColor.G, (byte)~newColor.B);
                Color1Pulse = Color.FromArgb((byte)~Color1Pulse.R, (byte)~Color1Pulse.G, (byte)~Color1Pulse.B);
                Color2Pulse = Color.FromArgb((byte)~Color2Pulse.R, (byte)~Color2Pulse.G, (byte)~Color2Pulse.B);


                Color3Pulse = Color.FromArgb((byte)~Color3Pulse.R, (byte)~Color3Pulse.G, (byte)~Color3Pulse.B);
                Color4Pulse = Color.FromArgb((byte)~Color4Pulse.R, (byte)~Color4Pulse.G, (byte)~Color4Pulse.B);
                Color5Pulse = Color.FromArgb((byte)~Color5Pulse.R, (byte)~Color5Pulse.G, (byte)~Color5Pulse.B);
                Color6Pulse = Color.FromArgb((byte)~Color6Pulse.R, (byte)~Color6Pulse.G, (byte)~Color6Pulse.B);
                Color7Pulse = Color.FromArgb((byte)~Color7Pulse.R, (byte)~Color7Pulse.G, (byte)~Color7Pulse.B);
                Color8Pulse = Color.FromArgb((byte)~Color8Pulse.R, (byte)~Color8Pulse.G, (byte)~Color8Pulse.B);
                Color9Pulse = Color.FromArgb((byte)~Color9Pulse.R, (byte)~Color9Pulse.G, (byte)~Color9Pulse.B);
                Color10Pulse = Color.FromArgb((byte)~Color10Pulse.R, (byte)~Color10Pulse.G, (byte)~Color10Pulse.B);

            }
            button3.BackColor = newColor;
            button2.BackColor = Color1Pulse;

            Colbutton2.BackColor = Color2Pulse;
            color3but.BackColor = Color3Pulse;
            button4.BackColor = Color4Pulse;
            button5.BackColor = Color5Pulse;
            button6.BackColor = Color6Pulse;

            button7.BackColor = Color7Pulse;
            button8.BackColor = Color8Pulse;
            button9.BackColor = Color9Pulse;
            button10.BackColor = Color10Pulse;


            staticClolor2.BackColor = newColor2;

            button2.Visible = false;
            Colbutton2.Visible = false;
            color3but.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            button7.Visible = false;
            button8.Visible = false;
            button9.Visible = false;
            button10.Visible = false;
            //Colbutton2.BackColor = Color2Pulse;
            if (xfer.UnknownR > 0 || xfer.UnknownG > 0 || xfer.UnknownB > 0)
            {
                crazy.Checked = true;
                UnknownRBut.Enabled = true;
                UnknownRBut.BackColor = UnknownRcol;
            }
            else
            {
                crazy.Checked = false;

                UnknownRBut.Enabled = false;
            }
            if (xfer.Color1.R > 0 || xfer.Color1.G > 0 || xfer.Color1.B > 0)
            {
                button2.Visible = true;
                button2.BackColor = Color1Pulse;
            }
            else
            {
                button2.Visible = false;
                goto done;
            }

            if (xfer.Color2.R > 0 || xfer.Color2.G > 0 || xfer.Color2.B > 0)
            {
                Colbutton2.Visible = true;
                Colbutton2.BackColor = Color2Pulse;
            }
            else
            {
                button2.Visible = false;
                Colbutton2.Visible = false;
                goto done;
            }

            if (xfer.Color3.R > 0 || xfer.Color3.G > 0 || xfer.Color3.B > 0)
            {

                color3but.Visible = true;
                color3but.BackColor = Color3Pulse;
            }
            else
            {

                color3but.Visible = false;
                goto done;
            }

            if (xfer.Color4.R > 0 || xfer.Color4.G > 0 || xfer.Color4.B > 0)
            {

                button4.Visible = true;
                button4.BackColor = Color4Pulse;
            }
            else
            {
                button4.Visible = false;
                goto done;

            }

            if (xfer.Color5.R > 0 || xfer.Color5.G > 0 || xfer.Color5.B > 0)
            {

                button5.Visible = true;
                button5.BackColor = Color5Pulse;
            }
            else
            {

                button5.Visible = false;
                goto done;
            }
            if (xfer.Color6.R > 0 || xfer.Color6.G > 0 || xfer.Color6.B > 0)
            {

                button6.Visible = true;
                button6.BackColor = Color6Pulse;
            }
            else
            {

                button6.Visible = false;
                goto done;
            }
            if (xfer.Color7.R > 0 || xfer.Color7.G > 0 || xfer.Color7.B > 0)
            {

                button7.Visible = true;
                button7.BackColor = Color7Pulse;
            }
            else
            {

                button7.Visible = false;
                goto done;
            }
            if (xfer.Color8.R > 0 || xfer.Color8.G > 0 || xfer.Color8.B > 0)
            {

                button8.Visible = true;
                button8.BackColor = Color8Pulse;

            }
            else
            {

                button8.Visible = false;
                goto done;
            }
            if (xfer.Color9.R > 0 || xfer.Color9.G > 0 || xfer.Color9.B > 0)
            {

                button9.Visible = true;
                button9.BackColor = Color9Pulse;
            }
            else
            {

                button9.Visible = false;
                goto done;
            }
            if (xfer.Color10.R > 0 || xfer.Color10.G > 0 || xfer.Color10.B > 0)
            {

                button10.Visible = true;
                button10.BackColor = Color10Pulse;
            }
            else
            {

                button10.Visible = false;
                goto done;
            }
        done:

            max2.Value = xfer.MaxRadius2;
            max3.Value = xfer.MaxRadius3;
            max4.Value = xfer.MaxRadius4;
            max5.Value = xfer.MaxRadius5;
            max6.Value = xfer.MaxRadius6;
            max7.Value = xfer.MaxRadius7;
            max8.Value = xfer.MaxRadius8;
            max9.Value = xfer.MaxRadius9;
            max10.Value = xfer.MaxRadius10;

            min2.Value = xfer.MinRadius2;
            min3.Value = xfer.MinRadius3;
            min4.Value = xfer.MinRadius4;
            min5.Value = xfer.MinRadius5;
            min6.Value = xfer.MinRadius6;
            min7.Value = xfer.MinRadius7;
            min8.Value = xfer.MinRadius8;
            min9.Value = xfer.MinRadius9;
            min10.Value = xfer.MinRadius10;


            if (PulseSpeedSingleNum.Value > 0)
            {
                PulsingBox.Checked = true;
                PulsingOptions.Enabled = true;
                StaticOptions.Enabled = false;
            }
            else
            {
                PulsingBox.Checked = false;
                PulsingOptions.Enabled = false;
            }

           
        }

        private void ColorLightEdit_Load(object sender, EventArgs e)
        {
            sizable();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            xfer.LightIntensity = (byte)numericUpDown1.Value;
            xfer.PulseSpeed = (byte)numericUpDown2.Value;
            xfer.ChangeIntensity = (byte)numericUpDown3.Value;

            xfer.LightRadius = (byte)outterSize.Value;
            xfer.type = shadow.Checked ? (byte)1 : (byte)0;

            xfer.PulseSpeedSingle = (byte)PulseSpeedSingleNum.Value;
            xfer.ChangeIntensitySingle = (byte)ChangeIntensitySingleNum.Value;
            xfer.Unknown10 = (byte)xxx.Value;
            byte RR2 = staticClolor2.BackColor.R;
            byte GG2 = staticClolor2.BackColor.G;
            byte BB2 = staticClolor2.BackColor.B;

            xfer.Unknown11 = (byte)unkwn11.Value;
            //xfer.UnknownR = crazy.Checked ? (byte)crazyNum.Value : (byte)0;
            Color UnknownRcol = UnknownRBut.BackColor;

            Color pulseColor1 = button2.BackColor;
            Color pulseColor2 = Colbutton2.BackColor;
            Color pulseColor3 = color3but.BackColor;
            Color pulseColor4 = button4.BackColor;
            Color pulseColor5 = button5.BackColor;
            Color pulseColor6 = button6.BackColor;
            Color pulseColor7 = button7.BackColor;
            Color pulseColor8 = button8.BackColor;
            Color pulseColor9 = button9.BackColor;
            Color pulseColor10 = button10.BackColor;

            Color gradColor = Color.FromArgb(staticClolor2.BackColor.R, staticClolor2.BackColor.G, staticClolor2.BackColor.B);
            Color baseColor = Color.FromArgb(button3.BackColor.R, button3.BackColor.G, button3.BackColor.B);
            gradColor = Color.FromArgb((byte)~gradColor.R, (byte)~gradColor.G, (byte)~gradColor.B);
            //UnknownRcol = Color.FromArgb((byte)~UnknownRcol.R, (byte)~UnknownRcol.G, (byte)~UnknownRcol.B);

            if (shadow.Checked)
            {
                baseColor = Color.FromArgb((byte)~baseColor.R, (byte)~baseColor.G, (byte)~baseColor.B);

                pulseColor1 = Color.FromArgb((byte)~pulseColor1.R, (byte)~pulseColor1.G, (byte)~pulseColor1.B);
                pulseColor2 = Color.FromArgb((byte)~pulseColor2.R, (byte)~pulseColor2.G, (byte)~pulseColor2.B);
                pulseColor3 = Color.FromArgb((byte)~pulseColor3.R, (byte)~pulseColor3.G, (byte)~pulseColor3.B);
                pulseColor4 = Color.FromArgb((byte)~pulseColor4.R, (byte)~pulseColor4.G, (byte)~pulseColor4.B);

                pulseColor5 = Color.FromArgb((byte)~pulseColor5.R, (byte)~pulseColor5.G, (byte)~pulseColor5.B);

                pulseColor6 = Color.FromArgb((byte)~pulseColor6.R, (byte)~pulseColor6.G, (byte)~pulseColor6.B);
                pulseColor7 = Color.FromArgb((byte)~pulseColor7.R, (byte)~pulseColor7.G, (byte)~pulseColor7.B);
                pulseColor8 = Color.FromArgb((byte)~pulseColor8.R, (byte)~pulseColor8.G, (byte)~pulseColor8.B);
                pulseColor9 = Color.FromArgb((byte)~pulseColor9.R, (byte)~pulseColor9.G, (byte)~pulseColor9.B);
                pulseColor10 = Color.FromArgb((byte)~pulseColor10.R, (byte)~pulseColor10.G, (byte)~pulseColor10.B);
                // UnknownRcol = Color.FromArgb((byte)~pulseColor2.R, (byte)~pulseColor2.G, (byte)~pulseColor2.B);
            }

            UnknownRcol = crazy.Checked ? UnknownRcol : Color.Black;

            xfer.R = baseColor.R;
            xfer.G = baseColor.G;
            xfer.B = baseColor.B;

            xfer.R2 = gradColor.R;
            xfer.G2 = gradColor.G;
            xfer.B2 = gradColor.B;

            xfer.UnknownR = UnknownRcol.R;
            xfer.UnknownG = UnknownRcol.G;
            xfer.UnknownB = UnknownRcol.B;

            xfer.Color1 = pulseColor1;
            xfer.Color2 = Colbutton2.Visible ? pulseColor2 : Color.Black;

            xfer.Color3 = color3but.Visible ? pulseColor3 : Color.Black;
            xfer.Color4 = button4.Visible ? pulseColor4 : Color.Black;
            xfer.Color5 = button5.Visible ? pulseColor5 : Color.Black;
            xfer.Color6 = button6.Visible ? pulseColor6 : Color.Black;

            xfer.Color7 = button7.Visible ? pulseColor7 : Color.Black;
            xfer.Color8 = button8.Visible ? pulseColor8 : Color.Black;
            xfer.Color9 = button9.Visible ? pulseColor9 : Color.Black;
            xfer.Color10 = button10.Visible ? pulseColor10 : Color.Black;


            xfer.MaxRadius2 = (byte)max2.Value;
            xfer.MinRadius2 = (byte)min2.Value;


            xfer.MaxRadius3 = (byte)max3.Value;
            xfer.MinRadius3 = (byte)min3.Value;

            xfer.MaxRadius4 = (byte)max4.Value;
            xfer.MinRadius4 = (byte)min4.Value;

            xfer.MaxRadius5 = (byte)max5.Value;
            xfer.MinRadius5 = (byte)min5.Value;


            xfer.MaxRadius6 = (byte)max6.Value;
            xfer.MinRadius6 = (byte)min6.Value;


            xfer.MaxRadius6 = (byte)max6.Value;
            xfer.MinRadius6 = (byte)min6.Value;


            xfer.MaxRadius7 = (byte)max7.Value;
            xfer.MinRadius7 = (byte)min7.Value;


            xfer.MaxRadius8 = (byte)max8.Value;
            xfer.MinRadius8 = (byte)min8.Value;


            xfer.MaxRadius9 = (byte)max9.Value;
            xfer.MinRadius9 = (byte)min9.Value;


            xfer.MaxRadius10 = (byte)max10.Value;
            xfer.MinRadius10 = (byte)min10.Value;



            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                button2.BackColor = colorDlg.Color;
                if (button2.BackColor == (shadow.Checked ? Color.White : Color.Black))
                {
                    button2.Visible = false;
                    Colbutton2.Visible = false;
                    color3but.Visible = false;
                    button4.Visible = false;
                    button5.Visible = false;
                    button6.Visible = false;
                    button7.Visible = false;
                    button8.Visible = false;
                    button9.Visible = false;
                    button10.Visible = false;
                }
                sizable();
            }
        }

        private void Colbutton2_Click(object sender, EventArgs e)
        {

            ColorDialog colorDlg = new ColorDialog();
            colorDlg.Color = Colbutton2.BackColor;
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                Colbutton2.BackColor = colorDlg.Color;
                if (Colbutton2.BackColor == (shadow.Checked ? Color.White : Color.Black))
                {
                    button2.Visible = false;
                    Colbutton2.Visible = false;
                    color3but.Visible = false;
                    button4.Visible = false;
                    button5.Visible = false;
                    button6.Visible = false;
                    button7.Visible = false;
                    button8.Visible = false;
                    button9.Visible = false;
                    button10.Visible = false;
                }
                sizable();
            }
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.Color = button3.BackColor;
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                button3.BackColor = colorDlg.Color;
            }
        }

        private void staticClolor2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This color shouldn't be changed in order to keep normal looking colorlight.");
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.Color = staticClolor2.BackColor;
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                staticClolor2.BackColor = colorDlg.Color;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void outterSize_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            StaticOptions.Enabled = true;
            PulsingOptions.Enabled = false;

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            StaticOptions.Enabled = false;
            PulsingOptions.Enabled = true;


        }

        private void staticPage_Click(object sender, EventArgs e)
        {

        }

        private void commonOptions_Enter(object sender, EventArgs e)
        {

        }

        private void UnknownRBut_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.Color = UnknownRBut.BackColor;
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                UnknownRBut.BackColor = colorDlg.Color;
                UnknownRcol = colorDlg.Color;
            }
        }

        private void crazy_CheckedChanged(object sender, EventArgs e)
        {
            UnknownRBut.Enabled = crazy.Checked ? true : false;
            UnknownRBut.BackColor = crazy.Checked ? UnknownRcol : Color.LightGray;
        }

        private void secCol_CheckedChanged(object sender, EventArgs e)
        {
            // Colbutton2.Enabled = secCol.Checked ? true : false;
            // Colbutton2.BackColor = secCol.Checked ? Color2Pulse : Color.LightGray;
        }

        private void xxx_ValueChanged(object sender, EventArgs e)
        {

        }

        private void color3but_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.Color = color3but.BackColor;
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                color3but.BackColor = colorDlg.Color;
                if (color3but.BackColor == (shadow.Checked ? Color.White : Color.Black))
                {
                    color3but.Visible = false;
                    button4.Visible = false;
                    button5.Visible = false;
                    button6.Visible = false;
                    button7.Visible = false;
                    button8.Visible = false;
                    button9.Visible = false;
                    button10.Visible = false;
                }
                sizable();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.Color = button4.BackColor;
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                button4.BackColor = colorDlg.Color;
                if (button4.BackColor == (shadow.Checked ? Color.White : Color.Black))
                {
                    button4.Visible = false;
                    button5.Visible = false;
                    button6.Visible = false;
                    button7.Visible = false;
                    button8.Visible = false;
                    button9.Visible = false;
                    button10.Visible = false;
                }
                sizable();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.Color = button5.BackColor;
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                button5.BackColor = colorDlg.Color;
                if (button5.BackColor == (shadow.Checked ? Color.White : Color.Black))
                {
                    button5.Visible = false;
                    button6.Visible = false;
                    button7.Visible = false;
                    button8.Visible = false;
                    button9.Visible = false;
                    button10.Visible = false;
                }
                sizable();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.Color = button6.BackColor;
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                button6.BackColor = colorDlg.Color;
                if (button6.BackColor == (shadow.Checked ? Color.White : Color.Black))
                {
                    button6.Visible = false;
                    button7.Visible = false;
                    button8.Visible = false;
                    button9.Visible = false;
                    button10.Visible = false;
                }
                sizable();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.Color = button7.BackColor;
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                button7.BackColor = colorDlg.Color;
                if (button7.BackColor == (shadow.Checked ? Color.White : Color.Black))
                {

                    button7.Visible = false;
                    button8.Visible = false;
                    button9.Visible = false;
                    button10.Visible = false;
                }

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.Color = button8.BackColor;
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                button8.BackColor = colorDlg.Color;
                if (button8.BackColor == (shadow.Checked ? Color.White : Color.Black))
                {

                    button8.Visible = false;
                    button9.Visible = false;
                    button10.Visible = false;
                }

            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.Color = button9.BackColor;
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                button9.BackColor = colorDlg.Color;
                if (button9.BackColor == (shadow.Checked ? Color.White : Color.Black))
                {

                    button9.Visible = false;
                    button10.Visible = false;
                }

            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.Color = button10.BackColor;
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                button10.BackColor = colorDlg.Color;
                if (button10.BackColor == (shadow.Checked ? Color.White : Color.Black))
                {

                    button10.Visible = false;
                }
            }
        }

        private void max2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void ChangeIntensitySingleNum_ValueChanged(object sender, EventArgs e)
        {
            if (ChangeIntensitySingleNum.Value <= xxx.Value)
                ChangeIntensitySingleNum.Value = xxx.Value + 1;
        }

        private void max3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void min2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void min4_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void PulseSpeedSingleNum_ValueChanged(object sender, EventArgs e)
        {

        }

        private void PulsingBox_CheckedChanged(object sender, EventArgs e)
        {
            if (PulsingBox.Checked)
            {
                StaticOptions.Enabled = false;
                PulsingOptions.Enabled = true;
                StaticOptions.Enabled = false;


                if (ChangeIntensitySingleNum.Value <= 0)
                    ChangeIntensitySingleNum.Value = numericUpDown1.Value;



                if (PulseSpeedSingleNum.Value <= 0)
                {
                    PulseSpeedSingleNum.Value = lastPulseVal;
                    PulseSpeedSingleNum.Minimum = 1;
                }

                if (xxx.Value <= 0)
                {
                    xxx.Value = 20;
                    xxx.Minimum = 1;
                }

            }
            else
            {
                lastPulseVal = (int)PulseSpeedSingleNum.Value;
                PulseSpeedSingleNum.Minimum = 0;

                StaticOptions.Enabled = true;
                PulsingOptions.Enabled = false;
                //numericUpDown1.Enabled = true;
                PulseSpeedSingleNum.Value = 0;
            }
        }

        private void min3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void AddColor_Click(object sender, EventArgs e)
        {


            if (button2.BackColor == (!shadow.Checked ? Color.Black : Color.White))
            {
                button2.Visible = false;
                Colbutton2.Visible = false;
            }

            if (Colbutton2.BackColor == (!shadow.Checked ? Color.Black : Color.White))
            {
                Colbutton2.Visible = false;
                button2.Visible = false;
            }

            if (color3but.BackColor == (!shadow.Checked ? Color.Black : Color.White))
                color3but.Visible = false;

            if (button4.BackColor == (!shadow.Checked ? Color.Black : Color.White))
                button4.Visible = false;

            if (button5.BackColor == (!shadow.Checked ? Color.Black : Color.White))
                button5.Visible = false;

            if (button6.BackColor == (!shadow.Checked ? Color.Black : Color.White))
                button6.Visible = false;

            if (button7.BackColor == (!shadow.Checked ? Color.Black : Color.White))
                button7.Visible = false;

            if (button8.BackColor == (!shadow.Checked ? Color.Black : Color.White))
                button8.Visible = false;


            if (button9.BackColor == (!shadow.Checked ? Color.Black : Color.White))
                button9.Visible = false;


            if (button10.BackColor == (!shadow.Checked ? Color.Black : Color.White))
                button10.Visible = false;




            if (!button2.Visible)
            {
                button2.BackColor = shadow.Checked ? Color.Black : Color.White;
                Colbutton2.BackColor = shadow.Checked ? Color.Black : Color.White;
                button2.Visible = true;
                Colbutton2.Visible = true;
                sizable();
                return;
            }
            if (!Colbutton2.Visible)
            {
                Colbutton2.BackColor = shadow.Checked ? Color.Black : Color.White;
                Colbutton2.Visible = true;
                sizable();
                return;
            }
            if (!color3but.Visible)
            {
                color3but.BackColor = shadow.Checked ? Color.Black : Color.White;
                color3but.Visible = true;
                sizable();
                return;
            }
            if (!button4.Visible)
            {
                button4.BackColor = shadow.Checked ? Color.Black : Color.White;
                button4.Visible = true;
                sizable();
                return;
            }
            if (!button5.Visible)
            {
                button5.BackColor = shadow.Checked ? Color.Black : Color.White;
                button5.Visible = true;
                sizable();
                return;
            }

            if (!button6.Visible)
            {
                button6.BackColor = shadow.Checked ? Color.Black : Color.White;
                button6.Visible = true;
                sizable();
                return;
            }
            if (!button7.Visible)
            {
                button7.BackColor = shadow.Checked ? Color.Black : Color.White;
                button7.Visible = true;
                sizable();
                return;
            }
            if (!button8.Visible)
            {
                button8.BackColor = shadow.Checked ? Color.Black : Color.White;
                button8.Visible = true;
                sizable();
                return;
            }
            if (!button9.Visible)
            {
                button9.BackColor = shadow.Checked ? Color.Black : Color.White;
                button9.Visible = true;
                sizable();
                return;
            }
            if (!button10.Visible)
            {
                button10.BackColor = shadow.Checked ? Color.Black : Color.White;
                button10.Visible = true;
                sizable();
                return;
            }

        }

        private void button11_Click(object sender, EventArgs e)
        {

            if (button10.Visible)
            {
                button10.Visible = false;
                sizable();
                return;
            }
            if (button9.Visible)
            {
                button9.Visible = false;
                sizable();
                return;
            }

            if (button8.Visible)
            {
                button8.Visible = false;
                sizable();
                return;
            }
            if (button7.Visible)
            {
                button7.Visible = false;
                sizable();

                return;
            }
            if (button6.Visible)
            {
                button6.Visible = false;
                sizable();
                return;
            }
            if (button5.Visible)
            {
                button5.Visible = false;
                sizable();
                return;
            }
            if (button4.Visible)
            {
                button4.Visible = false;
                sizable();
                return;

            }

            if (color3but.Visible)
            {
                color3but.Visible = false;
                sizable();
                return;
            }

            if (Colbutton2.Visible)
            {
                button2.Visible = false;
                Colbutton2.Visible = false;
                sizable();
                return;
            }
            if (button2.Visible)
            {
                button2.Visible = false;
                sizable();
                return;
            }

        }

        private void sequenceOptions_Enter(object sender, EventArgs e)
        {

        }
        private void shadow_CheckedChanged(object sender, EventArgs e)
        {
            if (shadow.Checked)
            {
                button2.BackColor = button2.BackColor.R == 255 && button2.BackColor.G == 255 && button2.BackColor.B == 255 ? Color.Black : button2.BackColor;
                Colbutton2.BackColor = Colbutton2.BackColor.R == 255 && Colbutton2.BackColor.G == 255 && Colbutton2.BackColor.B == 255 ? Color.Black : Colbutton2.BackColor;
                color3but.BackColor = color3but.BackColor == Color.White ? Color.Black : color3but.BackColor;
                button4.BackColor = button4.BackColor.R == 255 && button4.BackColor.G == 255 && button4.BackColor.B == 255 ? Color.Black : button4.BackColor;
                button5.BackColor = button5.BackColor == Color.White ? Color.Black : button5.BackColor;
                button6.BackColor = button6.BackColor == Color.White ? Color.Black : button6.BackColor;
                button7.BackColor = button7.BackColor == Color.White ? Color.Black : button7.BackColor;
                button8.BackColor = button8.BackColor == Color.White ? Color.Black : button8.BackColor;
                button9.BackColor = button9.BackColor == Color.White ? Color.Black : button9.BackColor;
                button10.BackColor = button10.BackColor == Color.White ? Color.Black : button10.BackColor;


            }
            else
            {

                button2.BackColor = button2.BackColor.R == 0 && button2.BackColor.G == 0 && button2.BackColor.B == 0 ? Color.White : button2.BackColor;
                Colbutton2.BackColor = Colbutton2.BackColor.R == 0 && Colbutton2.BackColor.G == 0 && Colbutton2.BackColor.B == 0 ? Color.White : Colbutton2.BackColor;
                color3but.BackColor = color3but.BackColor.R == 0 && color3but.BackColor.G == 0 && color3but.BackColor.B == 0 ? Color.White : color3but.BackColor;
                button4.BackColor = button4.BackColor.R == 0 && button4.BackColor.G == 0 && button4.BackColor.B == 0 ? Color.White : button4.BackColor;
                button5.BackColor = button5.BackColor.R == 0 && button5.BackColor.G == 0 && button5.BackColor.B == 0 ? Color.White : button5.BackColor;
                button6.BackColor = button6.BackColor.R == 0 && button6.BackColor.G == 0 && button6.BackColor.B == 0 ? Color.White : button6.BackColor;
                button7.BackColor = button7.BackColor.R == 0 && button7.BackColor.G == 0 && button7.BackColor.B == 0 ? Color.White : button7.BackColor;
                button8.BackColor = button8.BackColor.R == 0 && button8.BackColor.G == 0 && button8.BackColor.B == 0 ? Color.White : button8.BackColor;
                button9.BackColor = button9.BackColor.R == 0 && button9.BackColor.G == 0 && button9.BackColor.B == 0 ? Color.White : button9.BackColor;
                button10.BackColor = button10.BackColor.R == 0 && button10.BackColor.G == 0 && button10.BackColor.B == 0 ? Color.White : button10.BackColor;

            }
        }

        private void min6_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            if (this.Width == 355)
            {
                this.Width = 746;
                button11.Text = "-";
            }
            else
            {
                this.Width = 355;
                button11.Text = "+";
            }
        }

        private void shadow_Click(object sender, EventArgs e)
        {
            //turd2.Enabled = true;

        }



        private void button12_Click(object sender, EventArgs e)
        {

            button4.Focus();

            // if (button4.BackColor == Color.Black)
            MessageBox.Show(button4.BackColor.A.ToString());
            MessageBox.Show(button4.BackColor.ToKnownColor().ToString());
        }

        private void Colbutton2_VisibleChanged(object sender, EventArgs e)
        {
            if (Colbutton2.Visible)
            {
                numericUpDown2.Value = numericUpDown2.Value <= 0 ? 20 : numericUpDown2.Value;
                numericUpDown2.Minimum = 1;
                button3.Enabled = false;

            }
            else
            {
                numericUpDown2.Minimum = 0;
                button3.Enabled = true;
            }
        }
    }
}
