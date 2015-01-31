<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Proximamente.aspx.cs" Inherits="tamboprp.Proximamente" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>tamboprp | proximamente!</title>
    
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="css/font-awesome.css" rel="stylesheet" />
    <link href="css/ace-fonts.css" rel="stylesheet" />
    <link href="css/chosen.css" rel="stylesheet" />
    <link href="css/ui.jqgrid.css" rel="stylesheet" />
    <link href="css/ace.css" rel="stylesheet" />
    <link href="css/ace-part2.css" rel="stylesheet" />
    <link href="css/ace-skins.css" rel="stylesheet" />
    <link href="css/ace-rtl.css" rel="stylesheet" />
    <link href="css/ace-ie.css" rel="stylesheet" />

    <script src="js/ace-extra.js"></script>
    <script src="js/html5shiv.js"></script>
    <script src="js/respond.js"></script>
    <script src="js/jquery.js"></script>
    <script src="js/jquery1x.js"></script>
    <script src="js/bootstrap.js"></script>
    <script src="js/excanvas.js"></script>

</head>
<body class="no-skin">
    <form id="form1" runat="server">
    <div class="main-container" id="main-container">
        
        <div class="page-content">
                <!-- setting box goes here if needed -->

                <div class="row">
                  <div class="col-xs-2"></div>
                  <div class="col-xs-8">
        

                        <div>
                            <div class="page-header">
                            <h1><i class="menu-icon fa fa-rocket"></i> Proximamente!</h1>
                        </div>
                        <div class="row">
                            <h1 class="text-center">
                                <span class="ace-icon fa fa-check-square-o" aria-hidden="true"></span>
                                <span> tambo</span><strong class="text-primary">prp</strong>
                            </h1>
                            <div class="center">
                                <img src="img_tamboprp/corporativo/tamboprp1.png" />
                            </div>

                        </div>
                        <small><asp:Label ID="lblStatus" runat="server"></asp:Label></small>
        
                        </div>
                      <br/>
                      <br/>
                      <br/>
                      <br/>
                      
                      <div class="footer">
				            <div class="footer-inner">
					            <!-- #section:basics/footer -->
					            <div class="footer-content">
							        <p class="bigger-110 align-center"><span class="ace-icon fa fa-check-square-o" aria-hidden="true"></span><strong> tambo<span class="text-primary">prp</span></strong> | &copy; <%: DateTime.Now.Year %> todos los derechos reservados
                                    &nbsp; &nbsp;
						            <span class="action-buttons">
							            <a href="#"><i class="ace-icon fa fa-twitter-square light-blue bigger-150"></i></a>
                                        <a href="#"><i class="ace-icon fa fa-facebook-square text-primary bigger-150"></i></a>
                                        <a href="#"><i class="ace-icon fa fa-rss-square orange bigger-150"></i></a>
                                    </span>
						            </p>
					            </div>
					            <!-- /section:basics/footer -->
				            </div>
			            </div><!-- /.footer -->
                      
                      

                </div><!-- /.col -->
                    
                    
            <div class="col-xs-2"></div>
            </div><!-- /.row -->
        </div><!-- /.page-content -->
    </div>
    </form>
</body>
</html>
