
$(document).ready(function () {
    //var benefis = new Array();
    //$("#formCadastro #tbBenef TBODY TR").each(function () {
    //    var row = $(this);
    //    var benef = {};
    //    benef.CPF = row.find("TD").eq(0).html();
    //    benef.Nome = row.find("TD").eq(1).html();
    //    if (benef.CPF && benef.Nome) {
    //        benefis.push(benef);
    //    }
    //});

    $('#formCadastro').submit(function (e) {
        e.preventDefault();

        var cpf = $(this).find("#CPF").val();
        if (!TestaCPF(cpf)) {
            ModalDialog("Validação", "CPF inválido");
            return;
        }

        var benefis = new Array();
        $("#formCadastro #tbBenef TBODY TR").each(function () {
            var row = $(this);

            var benef = {};
            var id = row.find("TD").eq(4).html();
            benef.CPF = row.find("TD").eq(0).html();
            benef.Nome = row.find("TD").eq(1).html();
            benef.IsDeleted = false;
            benef.Id = id;

            if (row.hasClass('hidden')) {
                benef.IsDeleted = true;
            }

            if (benef.CPF && benef.Nome) {
                benefis.push(benef);
            }
        });

        $.ajax({
            url: urlPost,
            method: "POST",
            data: {
                "NOME": $(this).find("#Nome").val(),
                "CEP": $(this).find("#CEP").val(),
                "Email": $(this).find("#Email").val(),
                "Sobrenome": $(this).find("#Sobrenome").val(),
                "Nacionalidade": $(this).find("#Nacionalidade").val(),
                "Estado": $(this).find("#Estado").val(),
                "Cidade": $(this).find("#Cidade").val(),
                "Logradouro": $(this).find("#Logradouro").val(),
                "Telefone": $(this).find("#Telefone").val(),
                "CPF": $(this).find("#CPF").val(),
                "Benefiarios": benefis
            },
            error:
                function (r) {
                    if (r.status === 400)
                        ModalDialog("Ocorreu um erro", r.responseJSON);
                    else if (r.status == 500)
                        ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
                },
            success:
                function (r) {
                    ModalDialog("Sucesso!", r)
                    $("#formCadastro")[0].reset();
                }
        });
    })

});


function ModalDialog(titulo, texto) {
    var random = Math.random().toString().replace('.', '');
    var texto = '<div id="' + random + '" class="modal fade">                                                               ' +
        '        <div class="modal-dialog">                                                                                 ' +
        '            <div class="modal-content">                                                                            ' +
        '                <div class="modal-header">                                                                         ' +
        '                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>         ' +
        '                    <h4 class="modal-title">' + titulo + '</h4>                                                    ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-body">                                                                           ' +
        '                    <p>' + texto + '</p>                                                                           ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-footer">                                                                         ' +
        '                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>             ' +
        '                                                                                                                   ' +
        '                </div>                                                                                             ' +
        '            </div><!-- /.modal-content -->                                                                         ' +
        '  </div><!-- /.modal-dialog -->                                                                                    ' +
        '</div> <!-- /.modal -->                                                                                        ';

    $('body').append(texto);
    $('#' + random).modal('show');
}

function TestaCPF(strCPF) {
    var Soma;
    var Resto;
    Soma = 0;

     strCPF = strCPF.replace(/\./g, "").replace(/\-/g, "");

    if (strCPF === "00000000000") return false;

    for (i = 1; i <= 9; i++) Soma = Soma + parseInt(strCPF.substring(i - 1, i)) * (11 - i);
    Resto = (Soma * 10) % 11;

    if ((Resto == 10) || (Resto == 11)) Resto = 0;
    if (Resto != parseInt(strCPF.substring(9, 10))) return false;

    Soma = 0;
    for (i = 1; i <= 10; i++) Soma = Soma + parseInt(strCPF.substring(i - 1, i)) * (12 - i);
    Resto = (Soma * 10) % 11;

    if ((Resto == 10) || (Resto == 11)) Resto = 0;
    if (Resto != parseInt(strCPF.substring(10, 11))) return false;
    return true;
};
