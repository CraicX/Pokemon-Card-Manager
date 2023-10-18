var ps;
var toolTipdone = false;

export function initCardEffects() {

    startCardEffects();

    var container = document.getElementById('search-container');

    if (ps != null) ps.update(container);
}

export function addCardClick() {
    setCardClick();
}

export function parkToolSet() {
    $('.pokecard-tool-set').appendTo($('.toolset-parking'));
    $('.pokecard-tool-set').hide(); 
}

export function initPS() {
     ps = new PerfectScrollbar('#search-container', {
         wheelSpeed: 1,
         wheelPropagation: true,
         minScrollbarLength: 1
     });
}

export function updateResultCount(totalCount) {
    $('#searchText').text("Results: " + totalCount + " found");
}

export function hideSearching() {
    $('#searchSpinner').hide();
}

export function showSearching() {
    $('#searchSpinner').show();
}

export function setToolEvents() {

    $('.pokecard-div').off('mouseenter');
    $('.pokecard-div').on('mouseenter', function() {
        $('.pokecard-tool-set').appendTo($(this).find('.pokecard-tools'));
        $('.pokecard-tool-set').show();

        let cardId = $(this).data('card-id');

        if (cardId == null) return;

        DotNet.invokeMethodAsync('PokeCardManager', 'AssignToCard', cardId);
    });

    //if (!toolTipdone) {
    //    toolTipdone = true;
    //    $('.pokecard-div .sel-folder').off('click');
    //    $('.pokecard-div .sel-folder').on('click', function () {
    //        await showAlert(respObj.msg, 'success');
    //    });
    //}
}
