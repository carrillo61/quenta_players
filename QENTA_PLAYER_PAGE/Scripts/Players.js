var urlQentaPlayers = "http://localhost:38986/api/QentaPlayers"

$(document).ready(function () {
    GetPlayers()
});

$('#btnBuscarJugador').on('click', function () {
    var identificacion = document.getElementById('txtBuscarId').value;
    GetPlayer(identificacion);
});

$('#btnActualizarJugador').on('click', function () {
    var newPlayer = new Object();
    newPlayer.Id = document.getElementById('txtId').value;
    newPlayer.First_name = document.getElementById('txtFirst_name').value;
    newPlayer.Last_nam = document.getElementById('txtLast_nam').value;
    newPlayer.Position = document.getElementById('txtPosition').value;
    newPlayer.Height_feet = document.getElementById('txtHeight_feet').value;
    newPlayer.Height_inches = document.getElementById('txtHeight_inches').value;
    newPlayer.Weight_pounds = document.getElementById('txtWeight_pounds').value;

    UpdatePlayer(newPlayer.Id, JSON.stringify(newPlayer));
});

function GetPlayers() {
    $.getJSON(urlQentaPlayers, {
        tags: "mount rainier",
        tagmode: "any",
        format: "json",
        crossDomain: true
    }).done(function (data) {
        if (data != null) {
            var item = "";
            $('#tblPlayers tbody').html('');

            $.each(data, function (key, value) {
                item += "<tr><td>" + value.Id + "</td> <td>" + value.First_name + "</td> <td>" + value.Last_nam + "</td> <td>" + value.Position + "</td> <td>" + value.Height_feet + "</td> <td>" + value.Height_inches + "</td> <td>" + value.Weight_pounds + "</td></tr> ";
            });
            $('#tblPlayers tbody').append(item);
        }
    }).fail(function (jqXHR, textStatus, err) {
        alert("No se encontraron registros en la base de datos!!!");
    });
}

function GetPlayer(id) {
    $.getJSON(urlQentaPlayers + '/' + id, {
        tags: "mount rainier",
        tagmode: "any",
        format: "json",
        crossDomain: true
    }).done(function (data) {
        if (data != null) {
            LoadDataPlayer(data);
            var item = "";
            $('#tblPlayers tbody').html('');

            $.each(data, function (key, value) {
                item += "<tr><td>" + value.Id + "</td> <td>" + value.First_name + "</td> <td>" + value.Last_nam + "</td> <td>" + value.Position + "</td> <td>" + value.Height_feet + "</td> <td>" + value.Height_inches + "</td> <td>" + value.Weight_pounds + "</td></tr> ";
            });
            $('#tblPlayers tbody').append(item);
            
        }
    }).fail(function (jqXHR, textStatus, err) {
        alert("No se encontraron registros en la base de datos!!!");
    });
}

function UpdatePlayer(_idPlayer, player) {
    var url = urlQentaPlayers + "/" + _idPlayer;
    $.ajax({
        url: url,
        type: 'PUT',
        data: player,
        contentType: "application/json;chartset=utf-8",
        statusCode: {
            200: function (data) {
                if (data != undefined && data.Id > 0) {
                    LoadDataPlayer(data);

                    alert('El jugador fue actualizado con exito');
                }
            },
            404: function () {
                alert('Fallo en la actualización, jugador no encontrado');
            },
            400: function () {
                alert('Error');
            }
        }, success: function (data) {

        }
    });
}

function LoadDataPlayer(player) {
    if (player != null) {
        $('#txtId').val(player.Id);
        $('#txtFirst_name').val(player.First_name);
        $('#txtLast_nam').val(player.Last_nam);
        $('#txtPosition').val(player.Position);
        $('#txtHeight_feet').val(player.Height_feet);
        $('#txtHeight_inches').val(player.Height_inches);
        $('#txtWeight_pounds').val(player.Weight_pounds);
    }
}