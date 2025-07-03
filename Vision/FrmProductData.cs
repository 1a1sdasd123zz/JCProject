using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;

namespace Vision
{
    public partial class FrmProductData : Form
    {
        public FrmProductData()
        {
            InitializeComponent();
        }
        bool IsFirst;
        private void ImportData(string _csvPath)
        {
            try
           
            {
              
                ////Stopwatch stopwatch = new Stopwatch();
                //stopwatch.Start();
                string csvPath = _csvPath;
                DataTable dt = new DataTable();
                string contents = File.ReadAllText(csvPath, System.Text.Encoding.GetEncoding(0));
                TextFieldParser parser = new TextFieldParser(new StringReader(contents));
                parser.HasFieldsEnclosedInQuotes = true; parser.SetDelimiters(",");
                string[] fields;
                int ColumnCount = 0;
                int RowCount = 0;
                dataGridView.Rows.Clear();
                dataGridView.Columns.Clear();
                while (!parser.EndOfData)
                {
                    fields = parser.ReadFields();
                 
                 //   if (dt.Columns.Count == 0) 
                   if (dataGridView.Columns.Count ==0)
                    { 
                        foreach (string field in fields)
                        {
                            ColumnCount++;
                          //  dt.Columns.Add(new DataColumn(string.IsNullOrWhiteSpace(field.Trim('\"')) ? null : field.Trim('\"'), typeof(string)));
                            string data = string.IsNullOrWhiteSpace(field.Trim('\"')) ? null : field.Trim('\"');
                           
                            dataGridView.Columns.Add(ColumnCount.ToString(), data);
                           dataGridView.Columns[ColumnCount - 1].FillWeight = (float)(20);  //ch:因数据列数过长，需要设置列的FillWeight,FillWeight最大长为65535
                                                     
                       }
                    }
                    else 
                    {
                        RowCount++;
                     //   dt.Rows.Add(fields.Select(item => string.IsNullOrWhiteSpace(item.Trim('\"')) ? null : item.Trim('\"')).ToArray());
                        string[] data = fields.Select(item => string.IsNullOrWhiteSpace(item.Trim('\"')) ? null : item.Trim('\"')).ToArray();
                        dataGridView.Rows.Add();                      
                        for (int i = 0; i < data.Length; i++)
                        {
                            dataGridView.Rows[RowCount - 1].Cells[i].Value = data[i];
                        }
                    }               
                }
                                 
                parser.Close();
                //将读取的数据添加到datagridview
             
                //  dataGridView.DataSource = dt;

              
                //   MessageBox.Show(stopwatch.ElapsedMilliseconds.ToString());
            }
            catch (Exception ex) 
            { 
                MessageBox.Show("数据导入异常："+ex.ToString());
            }
        }
        /// <summary>
        /// 测试加载csv
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ToolStrip_Loaddxf_Click(object sender, EventArgs e)
        {
            string dataPath = "";
            using (OpenFileDialog openFile = new OpenFileDialog())
            {
                openFile.Title = "加载生产数据文件";
                openFile.Filter = "Data File(*.csv)|*.csv|ALL File(*.*)|*.*";

                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    //获得文件路径
                    dataPath = openFile.FileName;
                    ImportData(dataPath);                  
                }
            }

        }

        private void Frm_ProductData_Load(object sender, EventArgs e)
        {

            IsFirst = true;
        }
    }
}
