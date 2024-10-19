using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QL_DANHGIA
{
    public partial class frm_main : Form
    {
        private string connectionString = "Data Source=.;Initial Catalog=HeThongDanhGia;Integrated Security=True";

        private int SoHienTai_TraHoSo = 0;
        private int SoHienTai_SaoY = 0;
        private int SoHienTai_Ktmt = 0;
        private int SoHienTai_DiaChinh = 0;
        private int SoHienTai_ThuThueNN = 0;
        private int SoHienTai_HoTich = 0;

        public int loaiHoatDong;


        public frm_main()
        {
            InitializeComponent();
        }


        private void btTraHoSo_Click(object sender, EventArgs e)
        {
            SoHienTai_TraHoSo++;
            label_TraHS.Text = SoHienTai_TraHoSo.ToString();
            loaiHoatDong = 1;

        }

        private void bt_SaoY_Click(object sender, EventArgs e)
        {
            SoHienTai_SaoY++;
            label_SaoY.Text = SoHienTai_SaoY.ToString();
            loaiHoatDong = 2;

        }

        private void bt_KTMT_Click(object sender, EventArgs e)
        {
            SoHienTai_Ktmt++;
            label_ktmt.Text = SoHienTai_Ktmt.ToString();
            loaiHoatDong = 3;

        }

        private void bt_DiaChinh_Click(object sender, EventArgs e)
        {
            SoHienTai_DiaChinh++;
            label_diachinh.Text = SoHienTai_DiaChinh.ToString();
            loaiHoatDong = 4;

        }

        private void bt_ThuThueNN_Click(object sender, EventArgs e)
        {
            SoHienTai_ThuThueNN++;
            label_thuenongnghiep.Text = SoHienTai_ThuThueNN.ToString();
            loaiHoatDong = 5;

        }

        private void bt_HoTich_Click(object sender, EventArgs e)
        {
            SoHienTai_HoTich++;
            label_hotich.Text = SoHienTai_HoTich.ToString();
            loaiHoatDong = 6;


        }

        private void InSo_Click(object sender, EventArgs e)
        {

            // Truy vấn cơ sở dữ liệu và hiển thị dữ liệu trên form frm_QuaySo
            Frm_QuaySo frmQuaySo = new Frm_QuaySo(loaiHoatDong);
            frmQuaySo.Show();

        }

     }
}

