using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Vision.BaseClass
{

    class XmlTool
    {
        //将TreeView数据写入xlm
        private StreamWriter sr;
        XmlDocument doc = new XmlDocument();
        public void TreeViewToXml(TreeView treeView, string filePath)
        {
            XmlDocument xml = new XmlDocument();
            try
            {
                int rootNode = treeView.Nodes.Count;//获取当前treeView根节点
                if (File.Exists(filePath))
                {
                    xml.Load(filePath);
                    XmlNode xmlElement_class = xml.SelectSingleNode("WorkParameters");
                    xmlElement_class.RemoveAll();//删除所有节点 
                    for (int i = 0; i < rootNode; i++)
                    {
                        //  XmlElement xmlElement_workName = xml.CreateElement(treeView .Nodes [i].Text );//创建当前根节点名称
                        for (j = 0; j < treeView.Nodes[i].Nodes.Count; j++)   //循环当前根节点下所有子节点
                        {
                            XmlElement xmlElement_workName = xml.CreateElement(treeView.Nodes[i].Text);
                            xmlElement_workName.InnerText = treeView.Nodes[i].Nodes[j].Text;//创建根节点下子节点名称
                            xmlElement_class.AppendChild(xmlElement_workName);
                        }
                    }

                    xml.AppendChild(xmlElement_class);
                    xml.Save(filePath);
                }
                else
                {
                    // XmlNode root = doc.SelectSingleNode(filePath);
                    XmlElement xmlElement_class = xml.CreateElement("WorkParameters");//创建一个<class>节点  

                    for (int i = 0; i < rootNode; i++)
                    {

                        for (j = 0; j < treeView.Nodes[i].Nodes.Count; j++)   //循环当前根节点下所有子节点
                        {
                            XmlElement xmlElement_workName = xml.CreateElement(treeView.Nodes[i].Text);
                            xmlElement_workName.InnerText = treeView.Nodes[i].Nodes[j].Text;//创建根节点下子节点名称
                            xmlElement_class.AppendChild(xmlElement_workName);
                        }
                    }
                    xml.AppendChild(xml.CreateXmlDeclaration("1.0", "utf-8", ""));
                    xml.AppendChild(xmlElement_class);
                    xml.Save(filePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败：" + ex.Message);
            }

        }

        //将xlm数据导入TreeView

        public void XmlToTreeView(TreeView treeView, string filePath)
        {
            int ColumnCounts = treeView.Nodes.Count;
            XmlDocument xmlDocument = new XmlDocument();
            if (File.Exists(filePath))
            {
                xmlDocument.Load(filePath);
            }
            else
            {
                return;
            }

            try
            {
                XmlNodeList xmlNodeList = xmlDocument.SelectSingleNode("WorkParameters").ChildNodes;//选择class为根结点并得到旗下所有子节点  
                treeView.Nodes[0].Nodes.Clear();
                GlobalValue.Recipes.Clear();
                int i = 0;
                foreach (XmlNode xmlNode in xmlNodeList)//遍历class的所有节点  
                {
                    XmlElement xmlElement = (XmlElement)xmlNode;
                    treeView.Nodes[0].Nodes.Add(xmlElement.ChildNodes.Item(0).InnerText);
                    GlobalValue.Recipes.Add(xmlElement.ChildNodes.Item(0).InnerText);
                    i++;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("XML格式不对！" + ex.Message);
            }

        }
        /// <summary>
        /// 数据表中的数据写入xml
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="filePath"></param>
        public void DataGridViewToXml(DataGridView dataGridView, List<string> CloumnNames, String filePath)
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                if (File.Exists(filePath))
                {
                    xml.Load(filePath);
                    XmlNode xmlElement_class = xml.SelectSingleNode("WorkParameters");
                    xmlElement_class.RemoveAll();//删除所有节点             
                    for (int i = 0; i < dataGridView.Rows.Count; i++)    //得到总行数      
                    {
                        for (int j = 0; j < dataGridView.ColumnCount; j++) //得到总列数
                        {
                            XmlElement xmlElement_workName = xml.CreateElement(CloumnNames[j]);
                            xmlElement_workName.InnerText = dataGridView.Rows[i].Cells[j].Value.ToString();
                            xmlElement_class.AppendChild(xmlElement_workName);
                        }
                    }
                    xml.Save(filePath);
                }
                else
                {
                    XmlElement xmlElement_class = xml.CreateElement("WorkParameters");//创建一个<class>节点  
                    int row = dataGridView.Rows.Count;//得到总行数                           
                    for (int i = 0; i < row - 1; i++)//得到总行数并在之内循环      
                    {
                        for (int j = 0; j < dataGridView.ColumnCount; j++)//得到总列数，并循环创建列名称
                        {
                            XmlElement xmlElement_workName = xml.CreateElement(CloumnNames[j]);
                            xmlElement_workName.InnerText = dataGridView.Rows[i].Cells[j].Value.ToString();
                            xmlElement_class.AppendChild(xmlElement_workName);
                        }
                    }
                    xml.AppendChild(xml.CreateXmlDeclaration("1.0", "utf-8", ""));//编写文件头  
                    xml.AppendChild(xmlElement_class);//将这个<class>附到总文件头，而且设置为根结点  
                    xml.Save(filePath);//保存这个xml文件  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存出错" + ex.Message);
            }
        }
        int j;
        int i;
        int index;
        /// <summary>
        /// 导入参数值
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="ParamValues"></param>
        public void XmlToParamData(String filePath, string NodeName, ref List<string> ParamValues)
        {
            XmlDocument xmlDocument = new XmlDocument();
            if (File.Exists(filePath))
            {
                xmlDocument.Load(filePath);
            }
            else
            {
                return;
            }

            try
            {
                XmlNodeList xmlNodeList = xmlDocument.SelectSingleNode("WorkParameters").ChildNodes;//选择class为根结点并得到旗下所有子节点  
                                                                                                    // string ss;

                foreach (XmlNode xmlNode in xmlNodeList)//遍历class的所有节点  
                {
                    XmlElement xmlElement = (XmlElement)xmlNode;
                    //旗下的子节点<name>和<number>分别放入dataGridView

                    if (xmlNode.Name == NodeName)
                    {

                        ParamValues.Add(xmlElement.ChildNodes.Item(0).InnerText);

                    }
                    index += 1;

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("XML格式不对！" + ex.Message);
            }
        }
        /// <summary>
        /// 导入相机设置中的工站名
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="ParamValues"></param>
        public void XmlToWorkName(String filePath, ref List<string> ParamValues)
        {
            XmlDocument xmlDocument = new XmlDocument();
            if (File.Exists(filePath))
            {
                xmlDocument.Load(filePath);
            }
            else
            {
                return;
            }

            try
            {
                XmlNodeList xmlNodeList = xmlDocument.SelectSingleNode("WorkParameters").ChildNodes;//选择class为根结点并得到旗下所有子节点  
                                                                                                    // string ss;

                foreach (XmlNode xmlNode in xmlNodeList)//遍历class的所有节点  
                {
                    XmlElement xmlElement = (XmlElement)xmlNode;
                    //旗下的子节点<name>和<number>分别放入dataGridView

                    if (xmlNode.Name == "工位名")
                    {

                        ParamValues.Add(xmlElement.ChildNodes.Item(0).InnerText);

                    }

                    index += 1;

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("XML格式不对！" + ex.Message);
            }
        }
        /// <summary>
        /// 导入相机设置中的VPP
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="ParamValues"></param>
        public void XmlToVppName(String filePath, ref List<string> ParamValues)
        {
            XmlDocument xmlDocument = new XmlDocument();
            if (File.Exists(filePath))
            {
                xmlDocument.Load(filePath);
            }
            else
            {
                return;
            }

            try
            {
                XmlNodeList xmlNodeList = xmlDocument.SelectSingleNode("WorkParameters").ChildNodes;//选择class为根结点并得到旗下所有子节点  
                                                                                                    // string ss;

                foreach (XmlNode xmlNode in xmlNodeList)//遍历class的所有节点  
                {
                    XmlElement xmlElement = (XmlElement)xmlNode;
                    //旗下的子节点<name>和<number>分别放入dataGridView

                    if (xmlNode.Name == "对应Vpp")
                    {

                        ParamValues.Add(xmlElement.ChildNodes.Item(0).InnerText);

                    }

                    index += 1;

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("XML格式不对！" + ex.Message);
            }
        }
        /// <summary>
        /// 导入相机索引
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="ParamValues"></param>
        public void XmlToIntData(String filePath, string NodeNam, ref List<int> ParamValues)
        {
            XmlDocument xmlDocument = new XmlDocument();
            if (File.Exists(filePath))
            {
                xmlDocument.Load(filePath);
            }
            else
            {
                return;
            }

            try
            {
                XmlNodeList xmlNodeList = xmlDocument.SelectSingleNode("WorkParameters").ChildNodes;//选择class为根结点并得到旗下所有子节点  
                                                                                                    // string ss;

                foreach (XmlNode xmlNode in xmlNodeList)//遍历class的所有节点  
                {
                    XmlElement xmlElement = (XmlElement)xmlNode;
                    //旗下的子节点<name>和<number>分别放入dataGridView

                    if (xmlNode.Name == NodeNam)
                    {

                        ParamValues.Add(int.Parse(xmlElement.ChildNodes.Item(0).InnerText));

                    }

                    index += 1;

                }


            }

            catch (Exception ex)
            {
                MessageBox.Show("XML格式不对！" + ex.Message);
            }
        }
        /// <summary>
        /// 导入曝光
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="ParamValues"></param>
        public void XmlToDoubleData(String filePath, string NodeNam, ref List<double> ParamValues)
        {
            XmlDocument xmlDocument = new XmlDocument();
            if (File.Exists(filePath))
            {
                xmlDocument.Load(filePath);
            }
            else
            {
                return;
            }

            try
            {
                XmlNodeList xmlNodeList = xmlDocument.SelectSingleNode("WorkParameters").ChildNodes;//选择class为根结点并得到旗下所有子节点  
                                                                                                    // string ss;

                foreach (XmlNode xmlNode in xmlNodeList)//遍历class的所有节点  
                {
                    XmlElement xmlElement = (XmlElement)xmlNode;
                    //旗下的子节点<name>和<number>分别放入dataGridView

                    if (xmlNode.Name == NodeNam)
                    {

                        ParamValues.Add(double.Parse(xmlElement.ChildNodes.Item(0).InnerText));

                    }

                    index += 1;

                }


            }

            catch (Exception ex)
            {
                MessageBox.Show("XML格式不对！" + ex.Message);
            }
        }

        /// <summary>
        /// xml数据导入列表
        /// </summary>
        /// <param name="dataGridView">数据表</param>
        /// <param name="filePath">路径</param>
        public void XmlToDataGridView(DataGridView dataGridView, String filePath)

        {
            int ColumnCounts = dataGridView.ColumnCount;//获取数据表中的总列数
            XmlDocument xmlDocument = new XmlDocument();
            if (File.Exists(filePath))
            {
                xmlDocument.Load(filePath);
            }
            else
            {
                return;
            }

            try
            {
                XmlNodeList xmlNodeList = xmlDocument.SelectSingleNode("WorkParameters").ChildNodes;//选择class为根结点并得到旗下所有子节点  
                dataGridView.Rows.Clear();
                foreach (XmlNode xmlNode in xmlNodeList)//遍历class的所有节点  
                {
                    XmlElement xmlElement = (XmlElement)xmlNode;
                    //旗下的子节点<name>和<number>分别放入dataGridView

                    if (index < xmlNodeList.Count / ColumnCounts)
                    {
                        dataGridView.Rows.Add();//在dataGridView新加一行，并拿到改行的行标
                    }

                    if (index != 0 & index % ColumnCounts == 0)
                    {
                        i += 1;
                        j = 0;
                    }

                    dataGridView.Rows[i].Cells[j].Value = xmlElement.ChildNodes.Item(0).InnerText;//各个单元格分别添加  

                    j += 1;

                    index += 1;

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("XML格式不对！" + ex.Message);
            }

        }
        /// <summary>
        /// 往指定节点追加信息
        /// </summary>
        public void AppendIndex()
        {

        }
        public void DataGridViewToXml(DataGridView dataGridView, String filePath)
        {
            DataGridViewToXml(new DataGridViewExportOptions(dataGridView), filePath);
        }

        public void DataGridViewToXml(DataGridViewExportOptions dataGridViewExportOption, String filePath)
        {
            DataGridViewToXml(new List<DataGridViewExportOptions>(new DataGridViewExportOptions[] { dataGridViewExportOption }), filePath);
        }

        public void DataGridViewToXml(List<DataGridViewExportOptions> dataGridViewExportOptions, String filePath)
        {
            if (dataGridViewExportOptions == null || dataGridViewExportOptions.Count == 0) return;

            DataSet dataSet = new DataSet("Lists");

            int sheetIndex = 1;
            List<String> sheetNames = new List<String>();
            try
            {
                foreach (DataGridViewExportOptions option in dataGridViewExportOptions)
                {
                    #region " 处理在多个 DataGridView 设置为相同的表名称的问题 "
                    if (sheetNames.Contains(option.WorkSheetName))
                    {
                        int i = 1;
                        while (true)
                        {
                            string newSheetName = option.WorkSheetName + i.ToString();
                            if (!sheetNames.Contains(newSheetName))
                            {
                                sheetNames.Add(newSheetName);
                                option.WorkSheetName = newSheetName;
                                break;
                            }
                            i++;
                        }
                    }
                    else
                    {
                        sheetNames.Add(option.WorkSheetName);
                    }
                    DataGridViewFillToDataSet(dataSet, option);
                    sheetIndex++;
                    #endregion
                }
                ExportToXml(dataSet, filePath);
            }
            finally
            {
                dataSet.Dispose();
                GC.Collect();
            }
        }

        // 处理 DataGridView 中的数据以填充到指定的 DataSet 中    
        private void DataGridViewFillToDataSet(DataSet dataSet, DataGridViewExportOptions Option)
        {
            DataTable Table = new DataTable();
            Table.TableName = Option.WorkSheetName;

            if (Option.DataGridView == null) return;

            #region " 填充表头内容 "

            foreach (DataColumnExportOptions option in Option.VisibleColumnOptions)
            {
                if (!option.Visible) continue;
                Table.Columns.Add(new DataColumn(option.ColumnName));
            }

            #endregion

            #region " 填充表格正文内容 "

            foreach (DataGridViewRow dataRow in Option.DataGridView.Rows)
            {
                if (dataRow.IsNewRow) continue;

                DataRow Row = Table.NewRow();
                foreach (DataColumnExportOptions option in Option.VisibleColumnOptions)
                {
                    if (dataRow.Cells[option.ColumnName].Value == null)
                    {
                        Row[option.ColumnName] = "";
                    }
                    else
                    {
                        Row[option.ColumnName] = dataRow.Cells[option.ColumnName].Value.ToString();
                    }
                }
                Table.Rows.Add(Row);
            }

            #endregion
            dataSet.Tables.Add(Table);
        }

        public static TreeNode GetTreeNodeFormXml(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    TreeNode treeNode = new TreeNode();
                    XmlDocument doc = LoadXml(filePath);
                    RecursionTreeControl(doc.DocumentElement, treeNode);
                    return treeNode;
                }
                else
                {
                    return null;
                }
            }
            catch { throw; }

        }

        public static void SaveTreeViewToXml(TreeNode tn,string path)
        {
            XmlDocument xml = new XmlDocument();
            try
            {
                if (File.Exists(path))
                {
                    xml.Load(path);
                    xml.DocumentElement.RemoveAll();//删除所有节点 
                    CreateXmlElementFromTreeView(tn, xml.DocumentElement, xml);
                    xml.Save(path);
                    LogUtil.Log("配置保存成功。");
                }
            }
            catch (Exception ex) { LogUtil.LogError(ex.ToString()); }
        }

        public static void GetListFromXml(List<string> list,string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    XmlDocument doc = LoadXml(path);
                    XmlNode xn = doc.DocumentElement;
                    foreach(XmlNode item in xn.ChildNodes)
                    {
                        list.Add(item.LastChild.Value);
                    }
                }
            }
            catch { }
        }
    
        private static XmlDocument LoadXml(string path)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                XmlReaderSettings setting = new XmlReaderSettings();
                setting.IgnoreWhitespace = true;
                XmlReader reader = XmlReader.Create(path, setting);
                doc.Load(reader);
                reader.Close();
                return doc;
            }
            catch { throw; }
        }

        private static void CreateXmlElementFromTreeView(TreeNode tn, XmlElement xe,XmlDocument doc)
        {
            for (int i = 0; i < tn.Nodes.Count; i++)
            {
                XmlElement child = doc.CreateElement(tn.Nodes[i].Name);
                child.InnerText = tn.Nodes[i].Text;
                xe.AppendChild(child);
                CreateXmlElementFromTreeView(tn.Nodes[i], child, doc);
            }
        }

        /// <summary>
        /// RecursionTreeControl:表示将XML文件的内容添加在TreeView
        /// </summary>
        /// <param name="xmlNode">将要加载的XML文件中的节点元素</param>
        /// <param name="nodes">将要加载的XML文件中的节点集合</param>
        private static void RecursionTreeControl(XmlNode xmlNode, TreeNode nodes)
        {
            foreach (XmlNode node in xmlNode.ChildNodes)//循环遍历当前元素的子元素集合
            {
                TreeNode new_child = new TreeNode();//定义一个TreeNode节点对象
                new_child.Name = node.Name;
                new_child.Text = node.Value;
                nodes.Nodes.Add(new_child);//向当前TreeNodeCollection集合中添加当前节点
                RecursionTreeControl(node, new_child);
            }
        }

        #region " ExportToXml "
        /// <summary>    
        /// 保存 DataSet 数据到 Xml 文件    
        /// </summary>    
        /// <param name="dataSet">DataSet数据对象</param>    
        /// <param name="filePath">Xml 文件地址</param>
        private void ExportToXml(DataSet dataSet, String filePath)
        {
            #region " 判断文件是否存在，存在则删除原有文件 "
            try
            {
                if (File.Exists(filePath)) File.Delete(filePath);
            }
            catch
            {
                return;
            }
            #endregion

            dataSet.WriteXml(filePath);
        }
        #endregion

        #region ImportXML
        public void Xml2DataGridView(DataGridView dataGridView, String filePath)
        {
            System.Data.DataSet dataSet1 = new System.Data.DataSet(); ;
            dataGridView.Rows.Clear();
            dataSet1.ReadXml(filePath, XmlReadMode.Auto);

            DataTable dt = dataSet1.Tables[0];
            dataGridView.Rows.Add(dt.Rows.Count);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                object[] data = dt.Rows[i].ItemArray;
                for (int j = 0; j < data.Length; j++)
                {
                    dataGridView[j, i].Value = data[j];
                }
            }
        }
        #endregion


    }

    class IniFile
    {

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);


        public static void Write(string section, string key, string value,string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    File.Create(path);
                }
                WritePrivateProfileString(section, key, value, path);
            }
            catch { throw; }
        }

        public static string Read(string section, string key,string path)
        {
            try
            {
                StringBuilder SB = new StringBuilder(255);
                int i = GetPrivateProfileString(section, key, "", SB, 255, path);
                return SB.ToString();
            }
            catch { throw; }

        }
    }


    #region " DataColumnExportOptions "
    /// <summary>    
    /// 导出数据字段属性选项类    
    /// </summary>
    class DataColumnExportOptions
    {
        private String _ColumnName;
        private String _Caption;
        private Boolean _Visible;
        /// <summary>    
        /// 字段名称    
        /// </summary>
        public String ColumnName
        {
            get { return _ColumnName; }
            set { _ColumnName = value; }
        }
        /// <summary>    
        /// 字段标题    
        /// </summary>
        public String Caption
        {
            get { return _Caption; }
            set { _Caption = value; }
        }
        /// <summary>    
        /// 是否显示（导出）    
        /// </summary>
        public Boolean Visible
        {
            get { return _Visible; }
            set { _Visible = value; }
        }
        /// <summary>    
        /// 构造函数    
        /// </summary>    
        /// <param name="fColumnName">字段名称</param>
        public DataColumnExportOptions(String columnName)
            : this(columnName, columnName)
        {

        }
        /// <summary>    
        /// 构造函数    
        /// </summary>    
        /// <param name="fColumnName">字段名称</param>    
        /// <param name="fCaption">字段标题</param>
        public DataColumnExportOptions(String columnName, String caption)
            : this(columnName, caption, true)
        {

        }
        /// <summary>    
        /// 构造函数    
        /// </summary>    
        /// <param name="fColumnName">字段名称</param>    
        /// <param name="fCaption">字段标题</param>    
        /// <param name="fVisible">是否显示（导出）</param>
        public DataColumnExportOptions(String columnName, String caption, Boolean visible)
        {
            this._ColumnName = columnName;
            this._Caption = caption;
            this._Visible = visible;
        }
    }
    #endregion

    #region " DataGridViewExportOptions “

    class DataGridViewExportOptions
    {
        private DataGridView _DataGridView;
        private List<DataColumnExportOptions> _ColumnOptions;
        private List<DataColumnExportOptions> _VisibleColumnOptions;
        private String _WorkSheetName;

        /// <summary>    
        /// 要导出到DataGridView对象    
        /// </summary>
        public DataGridView DataGridView
        {
            get { return _DataGridView; }
            set { _DataGridView = value; }
        }
        /// <summary>    
        /// 导出的字段属性列表    
        /// </summary>
        public List<DataColumnExportOptions> ColumnOptions
        {
            get { return _ColumnOptions; }
            set { _ColumnOptions = value; }
        }
        /// <summary>    
        /// 要导出的字段列表（只读）    
        /// </summary>
        public List<DataColumnExportOptions> VisibleColumnOptions
        {
            get { return _VisibleColumnOptions; }
        }
        /// <summary>    
        /// 导出的工作表名称    
        /// </summary>
        public String WorkSheetName
        {
            get { return _WorkSheetName; }
            set { _WorkSheetName = value; }
        }
        /// <summary>    
        /// 构造函数    
        /// </summary>    
        /// <param name="dataGridView">要导出到DataGridView对象</param>
        public DataGridViewExportOptions(DataGridView dataGridView)
            : this(dataGridView, null)
        { }
        /// <summary>    
        /// 构造函数    
        /// </summary>    
        /// <param name="dataGridView">要导出到DataGridView对象</param>    
        /// <param name="columnOptions">导出的字段属性列表</param>
        public DataGridViewExportOptions(DataGridView dataGridView, List<DataColumnExportOptions> columnOptions)
            : this(dataGridView, columnOptions, null) { }
        /// <summary>    
        /// 构造函数    
        /// </summary>    
        /// <param name="dataGridView">要导出到DataGridView对象</param>    
        /// <param name="columnOptions">导出的字段属性列表</param>    
        /// <param name="workSheetName">导出生成的工作表名称</param>
        public DataGridViewExportOptions(DataGridView dataGridView, List<DataColumnExportOptions> columnOptions, String workSheetName)
        {
            if (dataGridView == null) return;

            this._DataGridView = dataGridView;
            if (columnOptions == null)
            {
                this._ColumnOptions = new List<DataColumnExportOptions>();
                foreach (DataGridViewColumn dataColumn in dataGridView.Columns)

                    this._ColumnOptions.Add(new DataColumnExportOptions(dataColumn.HeaderText, dataColumn.HeaderText, dataColumn.Visible));


            }
            else
            {
                this._ColumnOptions = columnOptions;
            }

            if (String.IsNullOrEmpty(workSheetName))
                this._WorkSheetName = dataGridView.Name;
            else
                this._WorkSheetName = workSheetName;

            this._VisibleColumnOptions = new List<DataColumnExportOptions>();
            foreach (DataColumnExportOptions option in this._ColumnOptions)
            {
                if (option.Visible)
                    this._VisibleColumnOptions.Add(option);
            }
        }
    }

#endregion



















        //XmlWriter xw;
        //XElement xElement;
        /// <summary>
        /// 创建XML
        /// </summary>
        /// <param name="xmlPath"></param>
     //   public void CreatXmlTree(string xmlPath, List<string> mWorkNames, List<string> mCamIndexs, List<string> mExposure)
     //   {
           
     //       for (int i=0;i<mWorkNames .Count ;i++)
     //       {
     //           xElement = new XElement(                          
     //                      new XElement("工位信息",
     //      new XElement("工位名称", mWorkNames[i]),
     //        new XElement("相机索引", mCamIndexs[i]),
     //          new XElement("曝光", mExposure[i])
          
     //     )


     //);
              
     //       }

     //       XmlWriterSettings settings = new XmlWriterSettings();
     //       settings.Encoding = new UTF8Encoding(false);
     //       settings.Indent = true;
     //       xw = XmlWriter.Create(xmlPath, settings);
     //       xw.Close();
     //       xw.Flush();
     //       xElement.Save(xw);
     //   }
    }

