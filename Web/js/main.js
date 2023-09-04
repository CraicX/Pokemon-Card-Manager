//  Main JS Funcs
var cBoundAsync;
var projObj;


async function bindObj() {
    if (!cBoundAsync) {
        await CefSharp.BindObjectAsync("boundAsync");
        cBoundAsync = await boundAsync;
    }
}

async function cPostPayload(payObj) {
    if (!payObj || !payObj.ObjName || !payObj.FuncName) {
        alert('cPostPayload called with wrong var');
        return;
    }
    await bindObj();
    await boundAsync.postPayload(JSON.stringify(payObj));
}

async function runCBUI(funcName) {
    await bindObj();
    await boundAsync[funcName]();
}

function jFetch(myurl, selector, append = false) {
    $.ajax({
        url: myurl,
        success: function (data) {
            if (!append) $(selector).html(data);
            else $(selector).append(data);
        }
    });
}

function jFetchCards(myurl, selector, append = false) {
    $.ajax({
        url: myurl,
        success: function (data) {
            if (!append) $(selector).html(data);
            else $(selector).append(data);

            startCardEffects();
        }
    });
}

$(function () {

    $('.logo-text').on("click", async function () {

        await bindObj();
        await cBoundAsync.openDebug();

    });

    $("#cardSearch").on("keypress", async function (evt) {
        // check if enter was pressed
        if (evt.keyCode == 13 && $(this).val() != '') {

            projObj = {
                "ObjName": "PCInterface",
                "FuncName": "ExecuteSearch",
                "Data": { "query": $(this).val() }
            };


            await CefSharp.BindObjectAsync("boundAsync");

            var jsonIn = await boundAsync.postPayload(JSON.stringify(projObj));


            $('.page-content').html(jsonIn);

            //var myPrj = await JSON.parse(jsonIn);

        }
    });

         


});