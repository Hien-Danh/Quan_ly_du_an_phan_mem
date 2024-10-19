using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace QL_DANHGIA
{
    public partial class Frm_QuaySo : Form
    {
        private int SoHienTai = 0;
        private string connectionString = "Data Source=.;Initial Catalog=HeThongDanhGia;Integrated Security=True";

        public int loaiHoatDong;


        public Frm_QuaySo(int loaiHoatDong)
        {
            InitializeComponent();
            label_ThoiGian.Text = DateTime.Now.ToString("dd/MM/yyyy");
            label_DangNhap.Text = DateTime.Now.ToString("HH:mm:ss");
            this.loaiHoatDong = loaiHoatDong;
            // Gọi phương thức LayDuLieuPhieu và Hiển thị dữ liệu
            DataTable dataTable = LayDuLieuPhieu(loaiHoatDong);
            HienThiDuLieu(dataTable);

        }

        public void HienThiDuLieu(DataTable dataTable)
        {
            // Kiểm tra nếu có ít nhất một dòng dữ liệu trong DataTable
            if (dataTable.Rows.Count > 0)
            {
                // Lặp qua mỗi dòng dữ liệu trong DataTable
                foreach (DataRow row in dataTable.Rows)
                {
                    // Lấy các giá trị từ cột IDBP, IDQuay, IDCB của mỗi dòng
                    int IDBP = Convert.ToInt32(row["IDBP"]);
                    int IDQuay = Convert.ToInt32(row["IDQuay"]);
                    int IDCB = Convert.ToInt32(row["IDCB"]);

                    // Gọi các phương thức LayTenBoPhan, LayHoTenCB, LayTenQuay để lấy dữ liệu tương ứng
                    string boPhan = LayTenBoPhan(IDBP);
                    string hoTen = LayHoTenCB(IDCB);
                    int quay = LayTenQuay(IDQuay);

                    // Hiển thị dữ liệu từ mỗi dòng lên các Label tương ứng
                    label_BoPhan.Text = boPhan;
                    label_ThoiGian.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    label_DangNhap.Text = DateTime.Now.ToString("HH:mm:ss");
                    label_Quay.Text = quay.ToString();
                    label_HoTen.Text = hoTen;
                    label_MaCB.Text = IDCB.ToString();
                }
            }
            else
            {
                // Nếu không có dữ liệu
                MessageBox.Show("Không có dữ liệu để hiển thị", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }



      
        private int LayTenQuay(int IDQuay)
        {
            int quay = -1;
            string query = "SELECT Quay FROM QUAY WHERE IDQuay = @IDQuay";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IDQuay", IDQuay);
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        quay = Convert.ToInt32(result);
                    }
                }
            }

            return quay;
        }
        private string LayTenBoPhan(int IDBP)
        {
            string tenBoPhan = string.Empty;
            string query = "SELECT BoPhan FROM BO_PHAN WHERE IDBP = @IDBP";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IDBP", IDBP);
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        tenBoPhan = result.ToString();
                    }
                }
            }

            return tenBoPhan;
        }
        private string LayHoTenCB(int IDCB)
        {
            string hoTen = string.Empty;
            string query = "SELECT HoTen FROM CAN_BO WHERE IDCB = @IDCB";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IDCB", IDCB);
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        hoTen = result.ToString();
                    }
                }
            }

            return hoTen;
        }


        private DataTable LayDuLieuPhieu(int loaiHoatDong)
        {
            
            DataTable dataTable = new DataTable();
            string query = @"
                SELECT 
                    PHIEU.*,
                    QUAY.Quay,
                    CAN_BO.HoTen,
                    BO_PHAN.BoPhan
                FROM 
                    PHIEU
                    INNER JOIN QUAY ON PHIEU.IDQuay = QUAY.IDQuay
                    INNER JOIN CAN_BO ON PHIEU.IDCB = CAN_BO.IDCB
                    INNER JOIN BO_PHAN ON PHIEU.IDBP = BO_PHAN.IDBP
                WHERE 
                    BO_PHAN.IDBP = @loaiHoatDong";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@loaiHoatDong", loaiHoatDong);
                        connection.Open();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dataTable;
        }

   

        private void bt_TiepTheo_Click(object sender, EventArgs e)
        {
            SoHienTai++;
            string formattedNumber = $"{SoHienTai:D2}"; 
            label_SoDangGoi.Text = formattedNumber;

        }

        private void bt_GoiLai_Click(object sender, EventArgs e)
        {
            label_SoDangGoi.Text = $"{SoHienTai:D2}";
        }

        private void Frm_QuaySo_Activated(object sender, EventArgs e)
        {
            // Tăng số 
            SoHienTai++;
            label_SoDangGoi.Text = $"{SoHienTai:D2}";
        }
    }
}
