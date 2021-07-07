


$(function () {
    $("#in").keyup(function (e) {
        //ths sing # is for id in the html page
        
        var searchGame = this.value; //this is cathce the input

        $.ajax({
            url: "/GameConsoles/Search", //in what controller to search
            data: { query: searchGame } // the name from the html (query) and the data we want (searchGame)

        }).done(function (data) {

            //first way
            $("tbody").html("");
            var temp = $("#searchTemp").html(); //take html from the id searchTemp in the view
            $.each(data, function (i, val)
            {
                var template = temp;
                $.each(val, function (key, value) {
                    template = template.replaceAll("{" + key + "}", value);
                });

                $("tbody").append(template);
            });

            
        });
    });
});

$(function () {
    $("#Userinput").keyup(function (e) {
        var searchInput = $("#Userinput").val();

        $.ajax({
            url: "/Games/Search", //in what controller to search
            data: { query: searchInput } // the name from the html (query) and the data we want 

        }).done(function (data) {

            //this work
            $("#tab").html(data); // tbody is the tag we want to fill from the data
            console.log(data);

            });
    });
});

$(function () {
    $("#UserinputAcss").keyup(function (e) {
        var searchInput = $("#UserinputAcss").val();

        $.ajax({
            url: "/Accessories/Search", //in what controller to search
            data: { query: searchInput } // the name from the html (query) and the data we want 

        }).done(function (data) {

            //this work
            $("#tabAcss").html(data); // the tag we want to fill from the data
            console.log(data);

        });
    });
});

$(function () {
    $("#UserToSearch").keyup(function (e) {
        var searchInput = $("#UserToSearch").val();

        $.ajax({
            url: "/Users/Search", //in what controller to search
            data: { query: searchInput } // the name from the html (query) and the data we want (searchInput)

        }).done(function (data) {

            //this work
            $("#userSearch").html(data); // the tag we want to fill from the data
            console.log(data);

        });
    });
});

