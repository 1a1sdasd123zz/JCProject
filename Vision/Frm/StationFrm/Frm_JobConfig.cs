using System;
using System.IO;
using System.Windows.Forms;
using Vision.BaseClass;
using Vision.BaseClass.VisionConfig;

namespace Vision.Frm.StationFrm
{
    public partial class Frm_JobConfig : Form
    {
        //private AuthorityInfo mAuthorityInfo;

        //private string authorityName;

        private JobCollection mJobs;

        public Frm_JobConfig(JobCollection jobCollection)//, AuthorityInfo _mAuthorityInfo, string _authorityName)
        {
            mJobs = jobCollection;
            //mAuthorityInfo = _mAuthorityInfo;
            //authorityName = _authorityName;
            InitializeComponent();
            InitDgv();
            UpdateDgv();
            InitCmb();
        }

        private void InitDgv()
        {
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToOrderColumns = false;
            dgv.RowHeadersVisible = false;
            dgv.Columns.Add("ID", "ID");
            dgv.Columns.Add("Name", "名称");
            DataGridViewCheckBoxColumn CheckBoxCol = new DataGridViewCheckBoxColumn();
            dgv.Columns.Add(CheckBoxCol);
            dgv.Columns[2].HeaderText = "是否加载";
            dgv.Columns[2].Name = "Loading";
            dgv.Columns.Add("Explain", "注释");
            dgv.Columns[0].Width = 50;
            dgv.Columns[0].ReadOnly = true;
            dgv.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[1].Width = 70;
            dgv.Columns[1].ReadOnly = true;
            dgv.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[2].Width = 50;
            dgv.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[3].Width = 200;
            dgv.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void InitCmb()
        {
            cmb_JobNames.Items.Clear();
            ComboBox.ObjectCollection items = cmb_JobNames.Items;
            object[] items2 = mJobs.JobInfoColl.JobInfos.GetKeys().ToArray();
            items.AddRange(items2);
            int index = mJobs.JobInfoColl.GetIDIndex(mJobs.CurrentID);
            cmb_JobNames.SelectedIndex = index;
        }

        private void UpdateDgv()
        {
            dgv.Rows.Clear();
            for (int i = 0; i < mJobs.Count; i++)
            {
                dgv.Rows.Add();
                dgv[0, i].Value = mJobs.JobInfoColl.JobInfos[i].ID;
                dgv[1, i].Value = mJobs.JobInfoColl.JobInfos[i].Name;
                dgv[2, i].Value = mJobs.JobInfoColl.JobInfos[i].IsLoaded;
                dgv[3, i].Value = mJobs.JobInfoColl.JobInfos[i].Explain;
            }
        }

        private void btn_LoadCurrentJob_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("是否切换作业 " + cmb_JobNames.SelectedItem.ToString() + "，切换作业前请将系统置为离线状态，切换可能花费时间较长请耐心等待……", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            if (dr == DialogResult.OK)
            {
                int index = mJobs.JobInfoColl.GetNameIndex(cmb_JobNames.SelectedItem.ToString());
                if (!mJobs.ChangeJob(mJobs.JobInfoColl.JobInfos[index].ID))
                {
                    cmb_JobNames.SelectedIndex = mJobs.JobInfoColl.GetIDIndex(mJobs.CurrentID);
                    MessageBox.Show($"切换型号：{mJobs.JobInfoColl.JobInfos[index].ID}失败！");
                }
                else
                {
                    MessageBox.Show($"切换型号：{mJobs.JobInfoColl.JobInfos[index].ID}成功！");
                }
            }
        }

        private void btn_CancelLoadCurrent_Click(object sender, EventArgs e)
        {
            cmb_JobNames.SelectedIndex = mJobs.JobInfoColl.GetIDIndex(mJobs.CurrentID);
        }

        private void tsBtn_NewLine_Click(object sender, EventArgs e)
        {
            ConfigNameForm configNameForm = new ConfigNameForm();
            configNameForm.ShowDialog();
            if (configNameForm.Flag)
            {
                if (!DgvContainsJobInfo(configNameForm.ID, configNameForm.Name))
                {
                    dgv.Rows.Add();
                    dgv[0, dgv.Rows.Count - 1].Value = configNameForm.ID;
                    dgv[1, dgv.Rows.Count - 1].Value = configNameForm.Name;
                    dgv[2, dgv.Rows.Count - 1].Value = true;
                    dgv[3, dgv.Rows.Count - 1].Value = "";
                    UpdateJobInfo();
                }
                else
                {
                    MessageBox.Show("ID或作业名重复，请重新输入！");
                }
            }
        }

        private bool DgvContainsJobInfo(int id, string name)
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if ((int)dgv[0, i].Value == id || dgv[1, i].Value.ToString() == name)
                {
                    return true;
                }
            }
            return false;
        }

        private void tsBtn_DeleteLine_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count <= 0)
            {
                return;
            }
            DialogResult dr = MessageBox.Show("是否删除作业：" + dgv[0, dgv.CurrentCell.RowIndex].Value.ToString() + " ?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            if (dr == DialogResult.OK)
            {
                if (dgv[1, dgv.CurrentCell.RowIndex].Value.ToString() == mJobs.CurrentName)
                {
                    MessageBox.Show("不能删除当前正在运行作业！");
                    return;
                }
                try
                {
                    if(Directory.Exists(mJobs.ProjectPath + dgv[0, dgv.CurrentCell.RowIndex].Value.ToString()))
                    {
                        Directory.Delete(mJobs.ProjectPath + dgv[0, dgv.CurrentCell.RowIndex].Value.ToString(), true);
                    }
                    mJobs.JobInfoColl.JobInfos.Remove(dgv[1, dgv.CurrentCell.RowIndex].Value.ToString());
                    mJobs.Jobs.Remove(dgv[1, dgv.CurrentCell.RowIndex].Value.ToString());
                    dgv.Rows.RemoveAt(dgv.CurrentCell.RowIndex);
                    mJobs.SaveJobInfos();
                }
                catch(Exception ex) { MessageBox.Show("删除作业异常！" + ex.ToString()); }

                //XmlHelp.WriteXML(mJobs.Jobs, Application.StartupPath + "\\Project\\MesDataConfig.xml", typeof(MesDatas));
            }
        }

        private void btn_LoadJobs_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("是否执行加载或移除作业，执行可能花费时间较长请耐心等待……", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            if (dr == DialogResult.OK)
            {
                UpdateJobInfo();
                MessageBox.Show("加载或移除作业完成！");
            }
        }

        private void UpdateJobInfo()
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                string name = dgv[1, i].Value.ToString();
                //string mesType = dgv[4, i].Value.ToString();
                string jobId = dgv[0, i].Value.ToString();
                string jobName = dgv[1, i].Value.ToString();
                bool loaded = (bool)dgv[2, i].EditedFormattedValue;
                //MyJobData job = mJobs.Jobs[mJobs.CurrentName];
                if (mJobs.JobInfoColl.JobInfos.ContainsKey(name))
                {
                    mJobs.JobInfoColl.JobInfos[name].IsLoaded = loaded;
                    mJobs.JobInfoColl.JobInfos[name].Explain = dgv[3, i].Value.ToString();
                }
                else
                {
                    JobInfo jobInfo = new JobInfo();
                    jobInfo.ID = (int)dgv[0, i].Value;
                    jobInfo.Name = dgv[1, i].Value.ToString();
                    jobInfo.IsLoaded = loaded;
                    jobInfo.Explain = dgv[3, i].Value.ToString();
                    mJobs.JobInfoColl.JobInfos.Add(jobInfo.Name, jobInfo);
                    BaseFile.CopyDirectory(mJobs.ProjectPath + mJobs.CurrentID, mJobs.ProjectPath + jobInfo.ID);
                }
                //if (!mJobs.mStatisticsInfo.JobNameList.Contains(name))
                //{
                //    Statistics mStatistics = new Statistics();
                //    mStatistics.JobName = name;
                //    mJobs.mStatisticsInfo.mStatisticsList.Add(mStatistics);
                //}
            }
            for (int j = 0; j < mJobs.Count; j++)
            {
                if (!DgvContainsJobInfo(mJobs.JobInfoColl.JobInfos[j].ID, mJobs.JobInfoColl.JobInfos[j].Name))
                {
                    if (mJobs.Jobs.ContainsKey(mJobs.JobInfoColl.JobInfos[j].Name))
                    {
                        mJobs.Jobs.Remove(mJobs.JobInfoColl.JobInfos[j].Name);
                    }
                    Directory.Delete(mJobs.ProjectPath + mJobs.JobInfoColl.JobInfos[j].ID, recursive: true);
                    mJobs.JobInfoColl.JobInfos.Remove(j);
                    j--;
                }
            }
            int index = mJobs.JobInfoColl.GetIDIndex(mJobs.CurrentID);
            mJobs.SaveJobInfos();
            //mJobs.mStatisticsInfo.SaveStatisticsNum();
            mJobs.AnalysisJobInfos();
            cmb_JobNames.Items.Clear();
            ComboBox.ObjectCollection items = cmb_JobNames.Items;
            object[] items2 = mJobs.JobInfoColl.JobInfos.GetKeys().ToArray();
            items.AddRange(items2);
            cmb_JobNames.SelectedIndex = index;
        }

        public static Type typen(string typeName)
        {
            Type type = null;
            //Assembly[] assemblyArray = AppDomain.CurrentDomain.GetAssemblies();
            //int assemblyArrayLength = assemblyArray.Length;
            //for (int j = 0; j < assemblyArrayLength; j++)
            //{
            //    type = assemblyArray[j].GetType(typeName);
            //    if (type != null)
            //    {
            //        return type;
            //    }
            //}
            //for (int i = 0; i < assemblyArrayLength; i++)
            //{
            //    Type[] typeArray = assemblyArray[i].GetTypes();
            //    int typeArrayLength = typeArray.Length;
            //    for (int k = 0; k < typeArrayLength; k++)
            //    {
            //        if (typeArray[k].Name.Equals(typeName))
            //        {
            //            return typeArray[k];
            //        }
            //    }
            //}
            return type;
        }

        private void btn_CancelLoad_Click(object sender, EventArgs e)
        {
            UpdateDgv();
        }

        private void dgv_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgv.IsCurrentCellDirty)
            {
                dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex == -1 || dgv.Rows[e.RowIndex].IsNewRow)
            {
                return;
            }
            //if (e.ColumnIndex == 2)
            //{
            //    DataGridViewCheckBoxCell cbCell = dgv[e.ColumnIndex + 2, e.RowIndex] as DataGridViewCheckBoxCell;
            //    if (!(bool)dgv[e.ColumnIndex, e.RowIndex].Value)
            //    {
            //        dgv[e.ColumnIndex + 1, e.RowIndex].ReadOnly = true;
            //        btnCell.ReadOnly = true;
            //    }
            //    else
            //    {
            //        dgv[e.ColumnIndex + 1, e.RowIndex].ReadOnly = false;
            //        btnCell.ReadOnly = false;
            //    }
            //}
        }
    }
}
