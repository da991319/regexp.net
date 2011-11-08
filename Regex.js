function SetOption() {
    var value = $("input[name=language]:checked").val();
    switch (value) {
        case "js":
            $(".JsOption").show();
            $(".NetOption").hide();
            break;
        case ".net":
            $(".NetOption").show();
            $(".JsOption").hide();
            break;
    }
}

function CheckReg() {
    var value = $("input[name=language]:checked").val();
    switch (value) {
        case "js":
            jsReg(options);
            break;
        case ".net":
            netReg();
            break;
    }
}

function jsReg() {
    if ($("#TxtSearchString").val().length > 0 && $("#TxtPattern").val().length > 0) {
        var modifiers = "";
        if ($("#IgnoreCase").is(":checked")) {
            modifiers = modifiers + "i";
        }

        if ($("#Multiline").is(":checked")) {
            modifiers = modifiers + "m";
        }

        if ($("#Global").is(":checked")) {
            modifiers = modifiers + "g"
        }

        var pattern = new RegExp($("#TxtPattern").val(), modifiers);
        if (pattern.test($("#TxtSearchString").val())) {
            $("#LabelResponse").html("");
            //var matches = pattern.match($("#TxtSearchString").val());
            var matches = $("#TxtSearchString").val().match(pattern);
            var i;
            for (i = 0; i < matches.length; i++) {
                $("#LabelResponse").append((i + 1) + ": " + matches[i] + "<br />");
            }
        }
        else {
            $("#LabelResponse").html("No Match found");
        }

    }
    else {
        $("#LabelResponse").html("");
    }
};

function netReg() {
    var input = $("#TxtSearchString").val();
    var pattern = $("#TxtPattern").val();
    var options = getOptions().join(",");

    $.ajax({
        type: "POST",
        url: "http://localhost:50451/RegExp.ashx?callback=?",
        data: { input: input, pattern: pattern, options: options },
        dataType: "jsonp",
        crossDomain: true,
        success: function (d) {
            var result = d;
            var i;

            if (result.length > 0) {
                $("#LabelResponse").html("");
                for (i = 0; i < result.length; i++) {
                    $("#LabelResponse").append((i + 1) + ": " + result[i] + "<br />");
                }
            }
            else {
                $("#LabelResponse").html("No Match found");
            }

        }
    });

};

function getOptions() {
    var options = [];
    $("#OptionsDiv input:checked").each(function () {
        options.push($(this).val());
    });

    return options;
}
