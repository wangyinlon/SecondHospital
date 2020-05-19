using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Flurl.Http;
using OpenAuth.Repository.Domain;

namespace TriageClient
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public double pWidth = 0;
        public double pHeight = 0;

        public MainWindow()
        {
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(("log4net.config")));
            InitializeComponent();
            pWidth = SystemParameters.PrimaryScreenWidth;//得到屏幕整体宽度
            pHeight = SystemParameters.PrimaryScreenHeight;//得到屏幕整体高度
            string scx = PrimaryScreen.ScaleX.ToString();
            string scx2 = PrimaryScreen.ScaleY.ToString();
            string scx3 = PrimaryScreen.DpiX.ToString();

        }
        //叫号数据源
        /// <summary>
        /// 普通号
        /// </summary>
        private List<OUTP_JZJLK> patientInfoList_0 = new List<OUTP_JZJLK>();
        /// <summary>
        /// 专家号
        /// </summary>
        private List<OUTP_JZJLK> patientInfoList_1 = new List<OUTP_JZJLK>();
        /// <summary>
        /// 复查号
        /// </summary>
        private List<OUTP_JZJLK> patientInfoList_2 = new List<OUTP_JZJLK>();
        #region 窗体加载




        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Configs.QueryDocLoginModel.Result.KSMC.Length >= 6)
            {
                KSMC.FontSize = 36;
            }
            KSMC.Text = Configs.QueryDocLoginModel.Result.KSMC;
            ZJMC.Text = Configs.QueryDocLoginModel.Result.ZJMC;
            ZJYSMC.Text = Configs.QueryDocLoginModel.Result.ZJYSMC;

            TextBlockCurrentPatient.Text = "";

            DataGridRow dgr = new DataGridRow();
            // DG1.Items.Add(new DataGridRow() { Item = new { GHXH = "1-1", HZXM = "2-2" } });
            //后台线程
            BackgroundWorker bk = new BackgroundWorker();
            bk.DoWork += delegate { Get(); };
            bk.RunWorkerCompleted += delegate
            {

            };
            bk.RunWorkerAsync();
        }
        #endregion

        private Apis _apis = new Apis();
        private void Get()
        {
        login:
            try
            {

                for (; ; )
                {
                    //20200224 测试医生代码 0776  0625
                    //YinLong.Framework.Logs.Log4.Debug("[获取列表]" + DateTime.Now.ToString());
                    //0普通，1专家，2复查
                    var list = _apis.QuerySignPatiend(Configs.QueryDocLoginModel.Result.ZJYSDM, DateTime.Now.ToString("yyyyMMdd"));//
                    //var list_0 = list.Where(x => x.GHLB == "0").ToList();
                    //var list_1 = list.Where(x => x.GHLB == "1").ToList();
                    //var list_2 = list.Where(x => x.GHLB == "2").ToList();
                    var listNew0 = list.Where(x => !patientInfoList_0.Select(z => z.PATID).Contains(x.PATID)).ToList();
                    //var listNew1 = list_1.Where(x => !patientInfoList_1.Select(z => z.PATID).Contains(x.PATID)).ToList();
                    //var listNew2 = list_2.Where(x => !patientInfoList_2.Select(z => z.PATID).Contains(x.PATID)).ToList();
                    //var listNew = list.Where(x => !patientInfoList.Select(z => z.PATID).Contains(x.PATID));
                    foreach (var expr in listNew0)
                    {
                        patientInfoList_0.Add(new OUTP_JZJLK
                        {
                            GHXH = expr.GHXH,
                            PATID = expr.PATID,
                            HZXM = expr.HZXM,
                            PatientState = expr.PatientState,
                        }
                        );
                    }
                    //foreach (var expr in listNew1)
                    //{
                    //    patientInfoList_1.Add(new OUTP_JZJLK
                    //    {
                    //        GHXH = expr.GHXH,
                    //        PATID = expr.PATID,
                    //        HZXM = expr.HZXM,
                    //        PatientState = expr.PatientState,
                    //    }
                    //    );
                    //}
                    //foreach (var expr in listNew2)
                    //{
                    //    patientInfoList_2.Add(new OUTP_JZJLK
                    //    {
                    //        GHXH = expr.GHXH,
                    //        PATID = expr.PATID,
                    //        HZXM = expr.HZXM,
                    //        PatientState = expr.PatientState,
                    //    }
                    //    );
                    //}
                    var count = patientInfoList_0.Where(x => x.PatientState == Configs.State_DengDaiJiaoHoa).Count();
                    this.Dispatcher.Invoke(new Action(delegate
                    {
                        this.DG1.ItemsSource = null; this.DG1.ItemsSource = patientInfoList_0;
                        //this.DG2.ItemsSource = null; this.DG2.ItemsSource = patientInfoList_1;
                        //this.DG3.ItemsSource = null; this.DG3.ItemsSource = patientInfoList_2;
                        TextBlockTip.Text = count.ToString();
                    }));
                    ResharhWait();


                    Thread.Sleep(5 * 1000);
                }
            }
            catch (Exception e)
            {
                Thread.Sleep(5 * 1000);
                //YinLong.Framework.Logs.Log4.Debug("获取最新叫号异常:" + e.ToString());
                goto login;
            }


        }
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Narrow_Click(object sender, RoutedEventArgs e)
        {
            SetWinsize();

        }
        private void Minwin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var element = (FrameworkElement)sender;
            if (e.ClickCount == 1)
            {
                var timer = new System.Timers.Timer(500);
                timer.AutoReset = false;
                timer.Elapsed += new ElapsedEventHandler((o, ex) => element.Dispatcher.Invoke(new Action(() =>
                {
                    var timer2 = (System.Timers.Timer)element.Tag;
                    timer2.Stop();
                    timer2.Dispose();
                    return;
                })));
                timer.Start();
                element.Tag = timer;
            }
            if (e.ClickCount > 1)
            {
                var timer = element.Tag as System.Timers.Timer;
                if (timer != null)
                {
                    timer.Stop();
                    timer.Dispose();
                    UIElement_DoubleClick(sender, e);
                }
            }
        }

        private void UIElement_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            SetWinsize();
        }


        private void Minwin_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();

            }
        }
        public void SetWinsize()
        {
            switch (Maxwin.Visibility)
            {
                case Visibility.Visible:
                    this.Width = 100;
                    this.Height = 100;
                    Maxwin.Visibility = Visibility.Collapsed;
                    Minwin.Visibility = Visibility.Visible;
                    break;
                case Visibility.Hidden:
                    break;
                case Visibility.Collapsed:
                    if (this.Left > pWidth - 560)
                    {
                        this.Left = pWidth - 570;
                        this.Width = 550;
                        this.Height = 520;
                        Maxwin.Visibility = Visibility.Visible;
                        Minwin.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        this.Width = 550;
                        this.Height = 520;
                        Maxwin.Visibility = Visibility.Visible;
                        Minwin.Visibility = Visibility.Collapsed;
                    }
                    if (this.Top > pHeight - 530)
                    {
                        this.Top = pHeight - 550;
                        this.Width = 550;
                        this.Height = 520;
                        Maxwin.Visibility = Visibility.Visible;
                        Minwin.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        this.Width = 550;
                        this.Height = 520;
                        Maxwin.Visibility = Visibility.Visible;
                        Minwin.Visibility = Visibility.Collapsed;
                    }
                    break;
                default:
                    break;
            }
        }



        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            SetWinsize();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new OnlyShowMessageBox().Show("请注意：前方高能，禁止入内！", false);
        }

        

        #region 普通叫号

        

        
        /// <summary>
        /// 叫号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonJiaoHao_OnClick(object sender, RoutedEventArgs e)
        {
            if (DG1.SelectedIndex < 0)
            {
                new OnlyShowMessageBox().Show("请选择病人！", false);
                return;
            }

            if (SubRowsId().PatientState == Configs.State_YiJiaoHoa)
            {
                new OnlyShowMessageBox().Show("已经叫过号了！", false);
                return;
            }

            if (_apis.PutPatiendCall(SubRowsId().PATID, SubRowsId().GHXH))
            {
                //请求服务器
                TextBlockCurrentPatient.Text = SubRowsId().HZXM;
                SubRowsId().PatientState = Configs.State_YiJiaoHoa;
                //广播叫号
                _apis.Call(SubRowsId().HZXM);
                //更新等待就诊
                ResharhWait();
            }
            else
            {
                new OnlyShowMessageBox().Show(Configs.State_JiaoHaoFail, false);
            }
        }
        #region 下一个

        /// <summary>
        /// 下一个
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonJiaoHaoNext_OnClick(object sender, RoutedEventArgs e)
        {
            var list = patientInfoList_0.FirstOrDefault(x => x.PatientState == Configs.PatientStateDefault);
            if (list == null)
            {
                TextBlockCurrentPatient.Text = "";
                new OnlyShowMessageBox().Show("没有需要叫号的病人！", false);
            }
            else
            {
                if (_apis.PutPatiendCall(list.PATID, list.GHXH))
                {
                    //请求服务器
                    TextBlockCurrentPatient.Text = list.HZXM;
                    list.PatientState = Configs.State_YiJiaoHoa;
                    //广播叫号
                    _apis.Call(SubRowsId().HZXM);
                    //更新等待就诊
                    ResharhWait();
                }
                else
                {
                    new OnlyShowMessageBox().Show(Configs.State_JiaoHaoFail, false);
                }

            }

        }
        private void ButtonJiaoHaoNext_ZJ_OnClick(object sender, RoutedEventArgs e)
        {
            var list = patientInfoList_1.FirstOrDefault(x => x.PatientState == Configs.PatientStateDefault);
            if (list == null)
            {
                TextBlockCurrentPatient_ZJ.Text = "";
                new OnlyShowMessageBox().Show("没有需要叫号的病人！", false);
            }
            else
            {
                if (_apis.PutPatiendCall(list.PATID, list.GHXH))
                {
                    //请求服务器
                    TextBlockCurrentPatient_ZJ.Text = list.HZXM;
                    list.PatientState = Configs.State_YiJiaoHoa;
                    //更新等待就诊
                    ResharhWait();
                }
                else
                {
                    new OnlyShowMessageBox().Show(Configs.State_JiaoHaoFail, false);
                }

            }

        }
        private void ButtonJiaoHaoNext_FC_OnClick(object sender, RoutedEventArgs e)
        {
            var list = patientInfoList_2.FirstOrDefault(x => x.PatientState == Configs.PatientStateDefault);
            if (list == null)
            {
                TextBlockCurrentPatient_FC.Text = "";
                new OnlyShowMessageBox().Show("没有需要叫号的病人！", false);
            }
            else
            {
                if (_apis.PutPatiendCall(list.PATID, list.GHXH))
                {
                    //请求服务器
                    TextBlockCurrentPatient_FC.Text = list.HZXM;
                    list.PatientState = Configs.State_YiJiaoHoa;
                    //更新等待就诊
                    ResharhWait();
                }
                else
                {
                    new OnlyShowMessageBox().Show(Configs.State_JiaoHaoFail, false);
                }

            }

        }
        #endregion
        #endregion

        #region 专家叫号




        private void ButtonJiaoHao_ZJ_OnClick(object sender, RoutedEventArgs e)
        {
            if (DG2.SelectedIndex < 0)
            {
                new OnlyShowMessageBox().Show("请选择病人！", false);
                return;
            }

            if (((OUTP_JZJLK)DG2.SelectedItem).PatientState == Configs.State_YiJiaoHoa)
            {
                new OnlyShowMessageBox().Show("已经叫过号了！", false);
                return;
            }

            if (_apis.PutPatiendCall(((OUTP_JZJLK)DG2.SelectedItem).PATID, SubRowsId().GHXH))
            {
                //请求服务器
                TextBlockCurrentPatient_ZJ.Text = ((OUTP_JZJLK)DG2.SelectedItem).HZXM;
                ((OUTP_JZJLK)DG2.SelectedItem).PatientState = Configs.State_YiJiaoHoa;
                //更新等待就诊
                ResharhWait();
            }
            else
            {
                new OnlyShowMessageBox().Show(Configs.State_JiaoHaoFail, false);
            }
        }
        #endregion

        #region 复查叫号




        private void ButtonJiaoHao_FC_OnClick(object sender, RoutedEventArgs e)
        {
            if (DG3.SelectedIndex < 0)
            {
                new OnlyShowMessageBox().Show("请选择病人！", false);
                return;
            }

            if (((OUTP_JZJLK)DG3.SelectedItem).PatientState == Configs.State_YiJiaoHoa)
            {
                new OnlyShowMessageBox().Show("已经叫过号了！", false);
                return;
            }

            if (_apis.PutPatiendCall(((OUTP_JZJLK)DG3.SelectedItem).PATID, SubRowsId().GHXH))
            {
                //请求服务器
                TextBlockCurrentPatient_FC.Text = ((OUTP_JZJLK)DG3.SelectedItem).HZXM;
                ((OUTP_JZJLK)DG3.SelectedItem).PatientState = Configs.State_YiJiaoHoa;
                //更新等待就诊
                ResharhWait();
            }
            else
            {
                new OnlyShowMessageBox().Show(Configs.State_JiaoHaoFail, false);
            }
        }
        #endregion

        

        #region 刷新等待叫号

        /// <summary>
        /// 刷新等待叫号
        /// </summary>
        private void ResharhWait()
        {
            var list = patientInfoList_0.Where(x => x.PatientState == Configs.PatientStateDefault).Take(2).ToList();
            if (list.Count == 0)
            {
                TextBlockWait1.Text = "";
                TextBlockWait2.Text = "";
            }
            else if (list.Count == 1)
            {
                TextBlockWait1.Text = list[0].HZXM;
                TextBlockWait2.Text = "";
            }
            else if (list.Count == 2)
            {
                TextBlockWait1.Text = list[0].HZXM;
                TextBlockWait2.Text = list[1].HZXM;
            }

        }
        #endregion

        #region 获取选中行的原始值
        /// <summary>
        /// 获取选中行的原始值
        /// </summary>
        /// <param name="rowindex"></param>
        private OUTP_JZJLK SubRowsId()
        {
            return (OUTP_JZJLK)DG1.SelectedItem;
        }
        #endregion

       /// <summary>
       /// 重新叫号
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>

        private void ButtonJiaoAgain_OnClick(object sender, RoutedEventArgs e)
        {
            if (DG1.SelectedIndex < 0)
            {
                new OnlyShowMessageBox().Show("请选择病人！", false);
                return;
            }

            if (_apis.PutPatiendCall(SubRowsId().PATID, SubRowsId().GHXH))
            {
                //请求服务器
                TextBlockCurrentPatient.Text = SubRowsId().HZXM;
                SubRowsId().PatientState = Configs.State_YiJiaoHoa;
                //更新等待就诊
                ResharhWait();
            }
            else
            {
                new OnlyShowMessageBox().Show(Configs.State_JiaoHaoFail, false);
            }
        }
    }
}
