using System;
using System.Web.Hosting;
using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;

namespace WebDashboard_CustomExport
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DashboardFileStorage dashboardFileStorage = new DashboardFileStorage("~/App_Data/Dashboards");
            ASPxDashboard1.SetDashboardStorage(dashboardFileStorage);

            DashboardSqlDataSource sqlDataSource = new DashboardSqlDataSource("SQL Data Source", "access97Connection");
            SelectQuery query = SelectQueryFluentBuilder
                .AddTable("SalesPerson")
                .SelectAllColumns()
                .Build("Sales Person");
            sqlDataSource.Queries.Add(query);

            DataSourceInMemoryStorage dataSourceStorage = new DataSourceInMemoryStorage();
            dataSourceStorage.RegisterDataSource("sqlDataSource", sqlDataSource.SaveToXml());
            ASPxDashboard1.SetDataSourceStorage(dataSourceStorage);
        }

        protected void ASPxDashboard1_ConfigureDataConnection(object sender, ConfigureDataConnectionWebEventArgs e) {
            if (e.ConnectionName == "access97Connection") {
                Access97ConnectionParameters access97Params = new Access97ConnectionParameters();
                access97Params.FileName = HostingEnvironment.MapPath(@"~/App_Data/nwind.mdb");
                e.ConnectionParameters = access97Params;
            }
        }
        protected void ASPxDashboard1_CustomExport(object sender, CustomExportWebEventArgs e)
        {
            XtraReport report = e.Report as XtraReport;
            PageHeaderBand headerBand = new PageHeaderBand();
            report.Bands.Add(headerBand);

            XRPictureBox icon = new XRPictureBox();
            icon.ImageUrl = @"~/App_Data/Images/dxlogo.png";
            icon.HeightF = 50;
            icon.WidthF = 300;
            headerBand.Controls.Add(icon);

            XRLabel customHeader = new XRLabel();
            customHeader.Text = "Additioanl Header Text";
            customHeader.LeftF = 300;
            customHeader.WidthF = 300;
            headerBand.Controls.Add(customHeader);         

            XRPageInfo dateInfo = new XRPageInfo();
            dateInfo.PageInfo = PageInfo.DateTime;
            dateInfo.Format = "Created at {0:h:mm tt dd MMMM yyyy}";
            dateInfo.TopF = 100;
            dateInfo.WidthF = 200;
            headerBand.Controls.Add(dateInfo);

            XRPageInfo userInfo = new XRPageInfo();
            userInfo.PageInfo = PageInfo.UserName;
            userInfo.Format = "Current User: {0}";
            userInfo.TopF = 100;
            userInfo.LeftF = 250;
            userInfo.WidthF = 200;
            headerBand.Controls.Add(userInfo);

            PageFooterBand footerBand = new PageFooterBand();
            report.Bands.Add(footerBand);
            XRPageInfo pageInfo = new XRPageInfo();
            pageInfo.Format = "Page {0} of {1}";
            footerBand.Controls.Add(pageInfo);
        }
    }
}