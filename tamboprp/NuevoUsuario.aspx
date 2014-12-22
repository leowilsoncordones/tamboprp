<%@ Page Title="tamboprp | usuario" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NuevoUsuario.aspx.cs" Inherits="tamboprp.NuevoUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
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
    <script src="js/jquery-ui.js"></script>
    <script src="js/jquery.js"></script>
    <script src="js/jquery1x.js"></script>
    <script src="js/excanvas.js"></script>
    <script src="js/bootstrap.js"></script>
    
    <script type="text/javascript">

        jQuery(document).ready(function () {
            var options = {
                onLoad: function () {
                    //$('#messages').text('Escribe tu contraseña');
                    $('#messages').text('');
                },
                onKeyUp: function (evt) {
                    $(evt.target).pwstrength("outputErrorList");
                }
            };
            $(':password').pwstrength(options);
        });

        (function ($) {
            "use strict";

            var options = {
                errors: [],
                // Options
                minChar: 8,
                errorMessages: {
                    password_to_short: "La contraseña es muy corta",
                    same_as_username: "La contraseña no puede ser igual que el nombre de usuario"
                },
                scores: [17, 26, 40, 50],
                verdicts: ["Débil", "Normal", "Medio", "Fuerte", "Muy fuerte"],
                showVerdicts: true,
                raisePower: 1.4,
                usernameField: "#username",
                onLoad: undefined,
                onKeyUp: undefined,
                viewports: {
                    progress: undefined,
                    verdict: undefined,
                    errors: undefined
                },
                // Rules stuff
                ruleScores: {
                    wordNotEmail: -100,
                    wordLength: -100,
                    wordSimilarToUsername: -100,
                    wordLowercase: 1,
                    wordUppercase: 3,
                    wordOneNumber: 3,
                    wordThreeNumbers: 5,
                    wordOneSpecialChar: 3,
                    wordTwoSpecialChar: 5,
                    wordUpperLowerCombo: 2,
                    wordLetterNumberCombo: 2,
                    wordLetterNumberCharCombo: 2
                },
                rules: {
                    wordNotEmail: true,
                    wordLength: true,
                    wordSimilarToUsername: true,
                    wordLowercase: true,
                    wordUppercase: true,
                    wordOneNumber: true,
                    wordThreeNumbers: true,
                    wordOneSpecialChar: true,
                    wordTwoSpecialChar: true,
                    wordUpperLowerCombo: true,
                    wordLetterNumberCombo: true,
                    wordLetterNumberCharCombo: true
                },
                validationRules: {
                    wordNotEmail: function (options, word, score) {
                        return word.match(/^([\w\!\#$\%\&\'\*\+\-\/\=\?\^\`{\|\}\~]+\.)*[\w\!\#$\%\&\'\*\+\-\/\=\?\^\`{\|\}\~]+@((((([a-z0-9]{1}[a-z0-9\-]{0,62}[a-z0-9]{1})|[a-z])\.)+[a-z]{2,6})|(\d{1,3}\.){3}\d{1,3}(\:\d{1,5})?)$/i) && score;
                    },
                    wordLength: function (options, word, score) {
                        var wordlen = word.length,
                            lenScore = Math.pow(wordlen, options.raisePower);
                        if (wordlen < options.minChar) {
                            lenScore = (lenScore + score);
                            options.errors.push(options.errorMessages.password_to_short);
                        }
                        return lenScore;
                    },
                    wordSimilarToUsername: function (options, word, score) {
                        var username = $(options.usernameField).val();
                        if (username && word.toLowerCase().match(username.toLowerCase())) {
                            options.errors.push(options.errorMessages.same_as_username);
                            return score;
                        }
                        return true;
                    },
                    wordLowercase: function (options, word, score) {
                        return word.match(/[a-z]/) && score;
                    },
                    wordUppercase: function (options, word, score) {
                        return word.match(/[A-Z]/) && score;
                    },
                    wordOneNumber: function (options, word, score) {
                        return word.match(/\d+/) && score;
                    },
                    wordThreeNumbers: function (options, word, score) {
                        return word.match(/(.*[0-9].*[0-9].*[0-9])/) && score;
                    },
                    wordOneSpecialChar: function (options, word, score) {
                        return word.match(/.[!,@,#,$,%,\^,&,*,?,_,~]/) && score;
                    },
                    wordTwoSpecialChar: function (options, word, score) {
                        return word.match(/(.*[!,@,#,$,%,\^,&,*,?,_,~].*[!,@,#,$,%,\^,&,*,?,_,~])/) && score;
                    },
                    wordUpperLowerCombo: function (options, word, score) {
                        return word.match(/([a-z].*[A-Z])|([A-Z].*[a-z])/) && score;
                    },
                    wordLetterNumberCombo: function (options, word, score) {
                        return word.match(/([a-zA-Z])/) && word.match(/([0-9])/) && score;
                    },
                    wordLetterNumberCharCombo: function (options, word, score) {
                        return word.match(/([a-zA-Z0-9].*[!,@,#,$,%,\^,&,*,?,_,~])|([!,@,#,$,%,\^,&,*,?,_,~].*[a-zA-Z0-9])/) && score;
                    }
                }
            },

                setProgressBar = function ($el, score) {
                    var options = $el.data("pwstrength"),
                        progressbar = options.progressbar,
                        $verdict;

                    if (options.showVerdicts) {
                        if (options.viewports.verdict) {
                            $verdict = $(options.viewports.verdict).find(".password-verdict");
                        } else {
                            $verdict = $el.parent().find(".password-verdict");
                            if ($verdict.length === 0) {
                                $verdict = $('<span class="password-verdict"></span>');
                                $verdict.insertAfter($el);
                            }
                        }
                    }

                    if (score < options.scores[0]) {
                        progressbar.addClass("progress-bar-danger").removeClass("progress-bar-warning").removeClass("progress-bar-success");
                        progressbar.find(".bar").css("width", "5%");
                        if (options.showVerdicts) {
                            $verdict.text(options.verdicts[0]);
                        }
                    } else if (score >= options.scores[0] && score < options.scores[1]) {
                        progressbar.addClass("progress-bar-danger").removeClass("progress-bar-warning").removeClass("progress-bar-success");
                        progressbar.find(".bar").css("width", "25%");
                        if (options.showVerdicts) {
                            $verdict.text(options.verdicts[1]);
                        }
                    } else if (score >= options.scores[1] && score < options.scores[2]) {
                        progressbar.addClass("progress-bar-warning").removeClass("progress-bar-danger").removeClass("progress-bar-success");
                        progressbar.find(".bar").css("width", "50%");
                        if (options.showVerdicts) {
                            $verdict.text(options.verdicts[2]);
                        }
                    } else if (score >= options.scores[2] && score < options.scores[3]) {
                        progressbar.addClas("progress-bar-warning").removeClass("progress-bar-danger").removeClass("progress-bar-success");
                        progressbar.find(".bar").css("width", "75%");
                        if (options.showVerdicts) {
                            $verdict.text(options.verdicts[3]);
                        }
                    } else if (score >= options.scores[3]) {
                        progressbar.addClass("progress-bar-success").removeClass("progress-bar-warning").removeClass("progress-bar-danger");
                        progressbar.find(".bar").css("width", "100%");
                        if (options.showVerdicts) {
                            $verdict.text(options.verdicts[4]);
                        }
                    }
                },

                calculateScore = function ($el) {
                    var self = this,
                        word = $el.val(),
                        totalScore = 0,
                        options = $el.data("pwstrength");

                    $.each(options.rules, function (rule, active) {
                        if (active === true) {
                            var score = options.ruleScores[rule],
                                result = options.validationRules[rule](options, word, score);
                            if (result) {
                                totalScore += result;
                            }
                        }
                    });
                    setProgressBar($el, totalScore);
                    return totalScore;
                },

                progressWidget = function () {
                    return '<div class="progress progress-striped"><div class="bar"></div></div>';
                },

                methods = {
                    init: function (settings) {
                        var self = this,
                            allOptions = $.extend(options, settings);

                        return this.each(function (idx, el) {
                            var $el = $(el),
                                progressbar,
                                verdict;

                            $el.data("pwstrength", allOptions);

                            $el.on("keyup", function (event) {
                                var options = $el.data("pwstrength");
                                options.errors = [];
                                calculateScore.call(self, $el);
                                if ($.isFunction(options.onKeyUp)) {
                                    options.onKeyUp(event);
                                }
                            });

                            progressbar = $(progressWidget());
                            if (allOptions.viewports.progress) {
                                $(allOptions.viewports.progress).append(progressbar);
                            } else {
                                progressbar.insertAfter($el);
                            }
                            progressbar.find(".bar").css("width", "0%");
                            $el.data("pwstrength").progressbar = progressbar;

                            if (allOptions.showVerdicts) {
                                verdict = $('<span class="password-verdict">' + allOptions.verdicts[0] + '</span>');
                                if (allOptions.viewports.verdict) {
                                    $(allOptions.viewports.verdict).append(verdict);
                                } else {
                                    verdict.insertAfter($el);
                                }
                            }

                            if ($.isFunction(allOptions.onLoad)) {
                                allOptions.onLoad();
                            }
                        });
                    },

                    destroy: function () {
                        this.each(function (idx, el) {
                            var $el = $(el);
                            $el.parent().find("span.password-verdict").remove();
                            $el.parent().find("div.progress").remove();
                            $el.parent().find("ul.error-list").remove();
                            $el.removeData("pwstrength");
                        });
                    },

                    forceUpdate: function () {
                        var self = this;
                        this.each(function (idx, el) {
                            var $el = $(el),
                                options = $el.data("pwstrength");
                            options.errors = [];
                            calculateScore.call(self, $el);
                        });
                    },

                    outputErrorList: function () {
                        this.each(function (idx, el) {
                            var output = '<ul class="error-list">',
                                $el = $(el),
                                errors = $el.data("pwstrength").errors,
                                viewports = $el.data("pwstrength").viewports,
                                verdict;
                            $el.parent().find("ul.error-list").remove();

                            if (errors.length > 0) {
                                $.each(errors, function (i, item) {
                                    output += '<li>' + item + '</li>';
                                });
                                output += '</ul>';
                                if (viewports.errors) {
                                    $(viewports.errors).html(output);
                                } else {
                                    output = $(output);
                                    verdict = $el.parent().find("span.password-verdict");
                                    if (verdict.length > 0) {
                                        el = verdict;
                                    }
                                    output.insertAfter(el);
                                }
                            }
                        });
                    },

                    addRule: function (name, method, score, active) {
                        this.each(function (idx, el) {
                            var options = $(el).data("pwstrength");
                            options.rules[name] = active;
                            options.ruleScores[name] = score;
                            options.validationRules[name] = method;
                        });
                    },

                    changeScore: function (rule, score) {
                        this.each(function (idx, el) {
                            $(el).data("pwstrength").ruleScores[rule] = score;
                        });
                    },

                    ruleActive: function (rule, active) {
                        this.each(function (idx, el) {
                            $(el).data("pwstrength").rules[rule] = active;
                        });
                    }
                };

            $.fn.pwstrength = function (method) {
                var result;
                if (methods[method]) {
                    result = methods[method].apply(this, Array.prototype.slice.call(arguments, 1));
                } else if (typeof method === "object" || !method) {
                    result = methods.init.apply(this, arguments);
                } else {
                    $.error("Method " + method + " does not exist on jQuery.pwstrength");
                }
                return result;
            };
        }(jQuery));

    </script>
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />  
    <div class="page-header">
        <h1><i class="menu-icon fa fa-user"></i> Ingreso de un nuevo usuario </h1>
    </div>

    <div class="row">
        <div class="col-sm-12">
            
            <!-- FORMULARIO -->
            <div id="formulario" class="form-horizontal">
                <!-- Nombre y Apellido -->
                <div class="form-group">
		            <label class="col-sm-3 control-label no-padding-right"> Nombre </label>
			        <div class="col-sm-3">
			            <input type="text" runat="server" id="fNombre" class="form-control col-xs-10 col-sm-5" />
			        </div>
		        </div>
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Apellido </label>
			        <div class="col-sm-3">
			            <input type="text" runat="server" id="fApellido" class="form-control col-xs-10 col-sm-5" />
			        </div>
                    <div class="col-sm-12"></div>
		        </div>
                <!-- Cargar foto -->
                <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Foto </label>
                    <div class="col-sm-3">
                        <label class="ace-file-input"><input type="file" id="id-input-file-2">
                            <span class="ace-file-container" data-title="Elegir">
                                <span class="ace-file-name" data-title="..."><i class=" ace-icon fa fa-upload"></i></span>
                            </span><a class="remove" href="#"><i class=" ace-icon fa fa-times"></i></a></label>
                    </div>
                    <div class="col-sm-12"></div>
		         </div>
                 <!-- Email -->
                 <div class="form-group">
                    <label class="col-sm-3 control-label no-padding-right"> Email </label>
                    <div class="col-sm-3">
			            <input type="email" runat="server" id="fEmail" placeholder="Ej. usuario@dominio.com" class="form-control col-xs-10 col-sm-5" />
			        </div>
                </div>
                <!-- Rol de usuario -->
                <div class="form-group">
		            <label class="col-sm-3 control-label no-padding-right"> Rol de usuario </label>
			        <div class="col-sm-3">
			            <asp:DropDownList ID="ddlRolUsuario" CssClass="form-control col-xs-10 col-sm-5" AutoPostBack="True" OnSelectedIndexChanged="ddlRolUsuario_SelectedIndexChanged" runat="server" ></asp:DropDownList>
			        </div>
                    <div class="col-sm-12"></div>
		        </div>
                <!-- Nickname y Password-->
                <div class="form-group">
		            <label class="col-sm-3 control-label no-padding-right"> Usuario </label>
			        <div class="col-sm-3">
			            <input type="text" runat="server" id="username" class="form-control col-xs-10 col-sm-5" />
			        </div>
		        </div>
                <div class="form-group">
		            <label class="col-sm-3 control-label no-padding-right"> Contraseña </label>
                    <div class="col-sm-3">
			            <input type="password" runat="server" id="password" class="form-control col-xs-10 col-sm-5" />
			        </div>
                    <!-- Chequeo de fortaleza de password-->
                    <div id="messages" class="col-sm-3">
                        <!-- string password checker -->
                    </div>
		        </div>
                <!-- Recomendación -->
                <div class="form-group">
		            <label class="col-sm-3 control-label no-padding-right"> Importante! </label>
			        <div class="col-sm-6">
			            <small>Por su seguridad, utilice contraseñas de al menos 8 caracteres de largo.<br/> 
                            Combine el uso de mayúsculas, minúsculas, números y caracteres especiales.</small>
			        </div>
		        </div>
                <!-- Botones -->
                <div class="form-group">
                    <div class="col-md-offset-3 col-md-9">
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-info" Text="Guardar" OnClick="btn_GuardarEvento" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnReset" runat="server" CssClass="btn btn-default" Text="Limpiar" OnClick="btn_LimpiarFormulario" />
				    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
