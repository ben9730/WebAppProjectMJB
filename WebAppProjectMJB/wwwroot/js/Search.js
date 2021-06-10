//$(function () {
//    $("#searchTry").submit(function (e) { //we search events in this case sumbit event 
//        //ths sing # is for id in the html page
//        e.preventDefault();

//        //console.log($("#in").val()); //this is cathce the input

//        var searchGame = $("#in").val(); //this is cathce the input
        
//        $.ajax({
//            url: "/GameConsoles/Search", //in what controller to search
//            data: { query: searchGame } // the name from the html (query) and the data we want (searchGame)
           
//        }).done(function (data) {
//            $("tbody").html(data); // tbody is the tag we want to fill from the data
//            console.log(data);
            
//        });
//    });
//});


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

            

            //2 way

            //var temp = $("#searchTemp").html(); //take html from the id searchTemp in the view
            //var tabel = "";
            //for (i = 0; i < data.length; i++)
            //{
            //    //replace the string {name} in the data[i]
            //    tabel += temp.replace("{name}",data[i].name);
            //}
            ////fill the data from the table
            //$("tbody").html(tabel);


            //final way

           // $("tbody").html(data); // tbody is the tag we want to fill from the data
            //console.log(data);
            //var tabel = "";
            //for (i = 0; i < data.length; i++)
            //{
            //    tabel += "<tr><td>" + data[i].name + "</td>" + "</tr>";
            //}
            //$("tbody").html(tabel);
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

