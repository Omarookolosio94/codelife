function isEmail(email) {
    const re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
}


function emailUsername(emailAddress) {
    return emailAddress.substring(0, emailAddress.indexOf("@"));
}

function getTodayDate() {
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = today.getFullYear();

    return today = dd + '/' + mm + '/' + yyyy;
}


//Redirect CountDown
function countDown(time, url, displayDiv, page) {
    var interval = setInterval(function () {
        var msg;

        if (time <= 2)
            msg = (time - 1) + " second";
        else
            msg = (time - 1) + " seconds";

        $(displayDiv).text("You will be redirected to " + page + " page in " + msg);
        time = time - 1;

        if (time == 0) {
            clearInterval(interval);
            window.location = url;
        }

    }, 1000);
}


function CloseAlertFail() {
    $('#alertFail').addClass("d-none");
    $('#alertFail').removeClass("show");
}

function CloseAlertSuccess() {
    $('#alertSuccess').addClass("d-none");
    $('#alertSuccess').removeClass("show");
}

function CloseAlertRedirect() {
    $('#alertRedirect').addClass("d-none");
    $('#alertRedirect').removeClass("show");
}



function checkCount(field, limit) {
    var tlength = field.val().length;
    field.val(field.val().substring(0, limit));
    var tlength = field.val().length;
    remain = limit - parseInt(tlength);
    return remain;
}


function DeleteArticle(articleId, authorId) {

    if (!window.confirm("This Article will be Deleted Permanently?")) {
        return;
    }

    $('#loader').removeClass("d-none");
    $("#deleteArticleBtn").attr("disabled", true);


    $.ajax({
        type: "POST",
        url: '../Article/DeleteArticle?articleId=' + articleId + '&authorId=' + authorId,
        contentType: 'application/json',
        success: onSuccessCall
    });

    function onSuccessCall(data) {
        if (data.success) {
            $('#loader').addClass("d-none");
            //$('#successMessage').text(data.message);
            //$('#alertSuccess').removeClass("d-none");
            //$('#alertSuccess').addClass("show");
            $("#deleteArticleBtn").attr("disabled", false);
            location.reload();
            return;
        } else {
            $('#loader').addClass("d-none");

            $('#failMessage').text(data.message);
            $('#alertFail').removeClass("d-none");
            $('#alertFail').addClass("show");
            $("#deleteArticleBtn").attr("disabled", false);

            return;
        }

    }
}


function DeleteProfile(authorId) {
    if (!window.confirm("Your Profile and all Articles Written by You will be deleted Permanently?")) {
        return;
    }

    $('#loader').removeClass("d-none");
    $("#deleteProfileBtn").attr("disabled", true);

    $.ajax({
        type: "POST",
        url: '../Profile/DeleteAuthor?authorId=' + authorId,
        contentType: 'application/json',
        success: onSuccessCall
    });

    function onSuccessCall(data) {
        if (data.success) {
            $('#loader').addClass("d-none");
            $('#successMessage').text(data.message);
            $('#alertSuccess').removeClass("d-none");
            $('#alertSuccess').addClass("show");

            setTimeout(function () {
                window.location.href = "../Home/Index";
            }, 3000);

            return;
        } else {
            $('#loader').addClass("d-none");

            $('#failMessage').text(data.message);
            $('#alertFail').removeClass("d-none");
            $('#alertFail').addClass("show");
            $("#deleteProfileBtn").attr("disabled", false);

            return;
        }

    }
}





