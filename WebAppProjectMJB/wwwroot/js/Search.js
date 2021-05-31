$(function () {
    $("#searchTry").submit(function (e) {
        //e.preventDefault();
        console.log($("#inp").val());

        var searchGame = $("#inp").val();

        //"/Games/Search",

        $.ajax({
            url: "/Games/Index",
            data: { Game: searchGame }
           
        }).done(function (data) {
            console.log(data);
            $("card").html(data);
        });
    });
});