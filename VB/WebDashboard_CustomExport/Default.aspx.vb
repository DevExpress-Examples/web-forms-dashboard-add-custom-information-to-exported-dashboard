Imports System
Imports System.Web.Hosting
Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardWeb
Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.DataAccess.Sql
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraReports.UI

Namespace WebDashboard_CustomExport
    Partial Public Class [Default]
        Inherits System.Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            Dim dashboardFileStorage As New DashboardFileStorage("~/App_Data/Dashboards")
            ASPxDashboard1.SetDashboardStorage(dashboardFileStorage)

            Dim sqlDataSource As New DashboardSqlDataSource("SQL Data Source", "access97Connection")
            Dim query As SelectQuery = SelectQueryFluentBuilder.
                AddTable("SalesPerson").
                SelectAllColumns().
                Build("Sales Person")
            sqlDataSource.Queries.Add(query)

            Dim dataSourceStorage As New DataSourceInMemoryStorage()
            dataSourceStorage.RegisterDataSource("sqlDataSource", sqlDataSource.SaveToXml())
            ASPxDashboard1.SetDataSourceStorage(dataSourceStorage)
        End Sub

        Protected Sub ASPxDashboard1_ConfigureDataConnection(ByVal sender As Object, _
                                                             ByVal e As ConfigureDataConnectionWebEventArgs)
            If e.ConnectionName = "access97Connection" Then
                Dim access97Params As New Access97ConnectionParameters()
                access97Params.FileName = HostingEnvironment.MapPath("~/App_Data/nwind.mdb")
                e.ConnectionParameters = access97Params
            End If
        End Sub
        Protected Sub ASPxDashboard1_CustomExport(ByVal sender As Object, _
                                                  ByVal e As CustomExportWebEventArgs)
            Dim report As XtraReport = TryCast(e.Report, XtraReport)
            Dim headerBand As New PageHeaderBand()
            report.Bands.Add(headerBand)

            Dim icon As New XRPictureBox()
            icon.ImageUrl = "~/App_Data/Images/dxlogo.png"
            icon.HeightF = 50
            icon.WidthF = 300
            headerBand.Controls.Add(icon)

            Dim customHeader As New XRLabel()
            customHeader.Text = "Additioanl Header Text"
            customHeader.LeftF = 300
            customHeader.WidthF = 300
            headerBand.Controls.Add(customHeader)

            Dim dateInfo As New XRPageInfo()
            dateInfo.PageInfo = pageInfo.DateTime
            dateInfo.Format = "Created at {0:h:mm tt dd MMMM yyyy}"
            dateInfo.TopF = 100
            dateInfo.WidthF = 200
            headerBand.Controls.Add(dateInfo)

            Dim userInfo As New XRPageInfo()
            userInfo.PageInfo = pageInfo.UserName
            userInfo.Format = "Current User: {0}"
            userInfo.TopF = 100
            userInfo.LeftF = 250
            userInfo.WidthF = 200
            headerBand.Controls.Add(userInfo)

            Dim footerBand As New PageFooterBand()
            report.Bands.Add(footerBand)
            Dim pageInfo_1 As New XRPageInfo()
            pageInfo_1.Format = "Page {0} of {1}"
            footerBand.Controls.Add(pageInfo_1)
        End Sub
    End Class
End Namespace