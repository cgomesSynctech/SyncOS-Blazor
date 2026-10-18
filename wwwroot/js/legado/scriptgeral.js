$('.money').focus(function () {
    var valor = $(this).val();
    valor = converteMoedaFloat(valor);
    $(this).val(converteFloatMoeda(valor));
    $(this).select();
});

$('.money').focusout(function () {
    var valor = $(this).val();
    valor = converteMoedaFloat(valor);
    $(this).val(converteFloatMoeda(valor));
});


$('.valor').focus(function () {
    $(this).select();
});

$('.valor').keypress(function (e) {
    return e.charCode >= 48 && event.charCode <= 57
    console.log(e);
});

//Evento global para POSTs via javascript
var sendRequest = function (url, type, content, callback) {
    //Exibe Espera
    var modal = $('<div />');
    modal.addClass("modal");
    $('body').append(modal);
    var loading = $(".loading");
    var popup = $('#popup');
    loading.show();
    popup.show();
    var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
    var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
    loading.css({ top: top, left: left });

    //Desabilitando links
    $('a').css({
        'pointer-events': 'none',
        'cursor': 'default',
        'text-decoration': 'none'
    });
    

    $.ajax({
        type: type,
        contentType: "application/json; charset=utf-8",
        url: url,
        data: (type == 'POST' ? content : null),
        dataType: "json",
        success: function (data) {
            $('a').css({
                'pointer-events': 'auto',
                'cursor': 'pointer',
                'text-decoration': 'none',
            });
            loading.hide();
            popup.hide();
            callback(null, data);
        },
        error: function (erro) {
            $('a').css({
                'pointer-events': 'auto',
                'cursor': 'pointer',
                'text-decoration': 'none',
            });
            loading.hide();
            popup.hide();
            callback(erro, null);
        }
    });
};

var converteMoedaFloat = function (valor) {

    if (valor === "") {
        valor = 0;
    } else {
        valor = valor.replace("R$", "");
        valor = valor.replace(".", "");
        valor = valor.replace(",", ".");
        valor = valor.replace(" ", "");
        valor = parseFloat(valor);
    }
    return valor;

};

var converteFloatMoeda = function(valor) {
    return valor.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' }).replace(" ", "").replace("R$", "");
}

var msgConfirm = function (msg, callBack) {
    bootbox.confirm({
        message: msg,
        title: 'Confirmação',
        backdrop: true,
        locale: 'br',
        callback: function (result) {
            callBack(result);
        }
    })
}

var modalList = function (titulo, lista, callBack) {
    var valores = lista.map(function (elemento) {
        return elemento.Value; 
    });
    var itens = lista.map(function (elemento) {
        return { value: elemento.Value, text: elemento.Text};
    });
    bootbox.prompt({
        title: titulo,
        inputType: 'select',
        multiple: false,
        centerVertical: true,
        locale: 'br',
        value: valores,
        inputOptions: itens,
        callback: function (result) {
            callBack(result);
        }
    })
}

var modalDatePrompt = function (titulo, cb) {    
    bootbox.prompt({
        title: titulo,
        inputType: 'date',
        callback: function (result) {
            cb(result);
        }
    });
}

var OpenUrl = function (url, newPage) {
    if (newPage)
        window.open(url);
    else
        window.location.href = url;
}

var viewBagJSONToObject = function (json) {
    var parser = new DOMParser;
    var dom = parser.parseFromString('<!doctype html><body>' + json, 'text/html');
    var obj = dom.body.textContent;
    return (obj != '' ? JSON.parse(obj) : null);
};

$(document).ready(function ($) {
    $('.form-control').keypress(function (e) {
        var key = e.which;
        if (key == 13)  // the enter key code
        {
            $('#botaoSalvar').click();
            return false;
        }
    });
});