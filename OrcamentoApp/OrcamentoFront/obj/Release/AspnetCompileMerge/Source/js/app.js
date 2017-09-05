angular.module('orcamentoApp', ['uiInputSiblings', 'uiPanelCollapsible', 'ui.router']);

$(function() {
    $('#side-menu').metisMenu();
});


//collapses the sidebar on window resize.
// Sets the min-height of #page-wrapper to window size
$(function() {
    $(window).bind("load resize", function() {
        var topOffset = 50;
        var width = (this.window.innerWidth > 0) ? this.window.innerWidth : this.screen.width;
        if (width < 768) {
            $('div.navbar-collapse').addClass('collapse');
            topOffset = 100; // 2-row-menu
        } else {
            $('div.navbar-collapse').removeClass('collapse');
        }

        var height = ((this.window.innerHeight > 0) ? this.window.innerHeight : this.screen.height) - 1;
        height = height - topOffset;
        if (height < 1) height = 1;
        if (height > topOffset) {   
            $("#page-wrapper").css("min-height", (height) + "px");
        }
    });

});

$(document).ready(function() {
    $(document).on('click', '#btn-collapse-menu', function() {
        if ($('#page-wrapper').css('margin-left') == '0px') {
            $('#page-wrapper').animate({marginLeft:'250px'},250, function() {$('#side-menu').show();});
            $.cookie("nav-collapse", false);
        }
        else {  
            $('#side-menu').hide();
            $('#page-wrapper').animate({marginLeft:'0'}, 250);
            $.cookie("nav-collapse", true);
        }
    });
});


$(document).on('change', ':file', function() {
    var input = $(this);
    var numFiles = input.get(0).files ? input.get(0).files.length : 1;
    if (!numFiles) {
        $("#input-files-label").hide();
        $("#button-upload-file").hide();
    }
    else {
        $("#input-files-label").show();
        $("#button-upload-file").show();
        $("#input-files-label").val(input[0].files[0].name);
    }
});

var exibeLoader = function() {
    $('body').append('<div class="loader"><div class="ball-scale-multiple"><div></div><div></div><div></div></div></div>');
}

var ocultaLoader = function() {
    $('.loader').remove();
}


