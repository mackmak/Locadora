var objValidacao = {
    controle: "",
    label: "",
    mensagem : ""
};

var params = new Array(objValidacao);

function validacaoTexto(params) {
    $("input[type=submit]").click(function() {
        $.each(params, function (key, value) {
            if ($(value.controle).val().length == 0) {
                $(value.label).text(value.mensagem);
                $(value.controle).siblings("input[type=hidden]").val("false");
            }
            else {
                $(value.label).text("");
                $(value.controle).siblings("input[type=hidden]").val("true");
            }
        })
    });
}

function validacaoCheckboxList(objValidacao) {
    $("input[type=submit]").click(function () {
        var marcado = false;
        $(objValidacao.controle).find("input[type='checkbox']:checked").each(function () {
            marcado = true;
            return false;
        });
        if (!marcado) {
            $(objValidacao.label).text(objValidacao.mensagem);
            $(objValidacao.controle).siblings("input[type=hidden]").val("false");//também pode ser feito com "input[name=hdnValidacao]"
        }
        else {
            $(objValidacao.label).text("");
            $(objValidacao.controle).siblings("input[type=hidden]").val("true");
        }

    });
}
function VarreduraValidacao(nomeHidden)
{
    nomeHidden = nomeHidden || "hdnValidacao";//valor padrão para os hiddens de validação
    $("input[name=" + nomeHidden + "]").each(function () {
        if (this.value == "false")
            return false;
    })

    return true;
}


