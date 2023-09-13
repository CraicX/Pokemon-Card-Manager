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
    cBoundAsync.postPayload(JSON.stringify(payObj));

    return;
}

async function cPostBack(payObj) {
    if (!payObj || !payObj.ObjName || !payObj.FuncName) {
        alert('cPostBack called with wrong var');
        return false;
    }
    await bindObj();
    return await cBoundAsync.postBack(JSON.stringify(payObj));
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

function validateFolderName() {
    if ($('#folderName').val() != '' && $('#folderType').find(":selected").val() != '') {
        $('#folderAdd').removeClass('disabled');
    } else {
        $('#folderAdd').addClass('disabled');
    }
}

function showAlert(msg, type = 'success') {
    const IconMap = {
        "success": "bx bx-check-circle",
        "error": "bx bx-x-circle",
        "warning": "bx bx-error",
        "info": "bx bx-info-circle",
        "primary": "bx bx-info-circle",
        "secondary": "bx bx-info-circle",
        "default": "bx bx-info-circle",
    };

    Lobibox.notify(type, {
        rounded: true,
        position: 'top right',
        showClass: 'fadeInScale',
        hideClass: 'zoomOut',
        icon: IconMap[type],
        delayIndicator: false,
        msg: msg
    });
};

$(function () {
    $('#themeIcon').on("click", async function () {
        alert('ok');
    });


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
        }
    });

    $('#folderName').on("change", async function (evt) { validateFolderName(); });
    $('#folderType').on("change", async function (evt) { validateFolderName(); });

    $('#folderAdd').on("click", async function (evt) {
        projObj = {
            "ObjName": "PCInterface",
            "FuncName": "CreateFolder",
            "Data": {
                "folderName": $('#folderName').val(),
                "folderType": $('#folderType').find(":selected").val()
            }
        };

        let resp = await cPostBack(projObj);

        if (resp) {
            let respObj = await JSON.parse(resp);
            if (respObj.status == 'success') {
                await showAlert(respObj.msg, 'success');

                await $('#createFolderModal').modal('hide');

                await jFetch('list-folders.app', '#folderWrap');

            } else {
                await showAlert(respObj.msg, 'error');
            }
        }
    });

});