Pt = (o, e = -20, t = 20) => Math.min(Math.max(o, e), t);
var MvAuto = false;
var LastCard = null;
var CurrentDeg = 0;
var parentOffset = null;
var ZoomCard = null;


function OrientCard(Xdeg, Ydeg) {
    if (!MvAuto) {
        var acard = LastCard[0];// document.getElementById("acard");
        acard.style.setProperty('--rx', Xdeg + 'deg');
        acard.style.setProperty('--rotate-x', Xdeg + 'deg');
        acard.style.setProperty('--ry', Ydeg + 'deg');
        acard.style.setProperty('--rotate-y', Ydeg + 'deg');
        acard.style.setProperty('--mx', 40 - (Xdeg * 5) + '%');
        acard.style.setProperty('--pointer-x', 40 - (Xdeg * 5) + '%');
        acard.style.setProperty('--my', 5 + Ydeg + '%');
        acard.style.setProperty('--pointer-y', 5 + Ydeg + '%');
        acard.style.setProperty('--tx', Xdeg + 'px');
        acard.style.setProperty('--translate-x', Xdeg + 'px');
        acard.style.setProperty('--ty', Ydeg / 10 + 'px');
        acard.style.setProperty('--translate-y', Ydeg / 10 + 'px');
        acard.style.setProperty('--pos', Ydeg * 5 + '% ' + Xdeg + '% ');
        acard.style.setProperty('--posx', 50 + Ydeg / 10 + Xdeg + '% ');
        acard.style.setProperty('--background-x', 50 + Ydeg / 10 + Xdeg + '% ');
        acard.style.setProperty('--posy', 50 + Xdeg / 10 + Ydeg / 10 + '% ');
        acard.style.setProperty('--background-y', 50 + Xdeg / 10 + Ydeg / 10 + '% ');
        acard.style.setProperty('--hyp', Pt(Math.sqrt((Xdeg - 50) * (Xdeg - 50) + (Ydeg - 50) * (Ydeg - 50)) / 50, 0, 1));
        acard.style.setProperty('--pointer-from-center', Pt(Math.sqrt((Xdeg - 50) * (Xdeg - 50) + (Ydeg - 50) * (Ydeg - 50)) / 50, 0, 1));
    }
}

    function ShineCard(e) {
        let force = 10;

        if (ZoomCard != null) force = 30;

        const offsetY = -((e.pageY - LastCard.offset().top) - LastCard.height() / 2) / force;
        const offsetX = ((e.pageX - LastCard.offset().left) - LastCard.width() / 2) / force;

        let pointerX = ((e.pageX - LastCard.offset().left) / LastCard.width()) * 100;
        let pointerY = ((e.pageY - LastCard.offset().top) / LastCard.height()) * 100;
        //let offsetY = pointerY;
        //let offsetX = pointerX;

        var acard = LastCard[0];
        acard.style.setProperty('--rx', offsetX + 'deg');
        acard.style.setProperty('--rotate-x', offsetX + 'deg');
        acard.style.setProperty('--ry', offsetY + 'deg');
        acard.style.setProperty('--rotate-y', offsetY + 'deg');
        acard.style.setProperty('--mx', 40 - (offsetX * 5) + '%');
        acard.style.setProperty('--pointer-x', pointerX + '%');
        acard.style.setProperty('--my', 5 + offsetY + '%');
        acard.style.setProperty('--pointer-y', pointerY + '%');
        acard.style.setProperty('--tx', offsetX + 'px');
        acard.style.setProperty('--translate-x', offsetX + 'px');
        acard.style.setProperty('--ty', offsetY / 10 + 'px');
        acard.style.setProperty('--translate-y', offsetY / 10 + 'px');
        acard.style.setProperty('--pos', offsetY * 5 + '% ' + offsetX + '% ');
        acard.style.setProperty('--posx', 50 + offsetY / 10 + offsetX + '% ');
        acard.style.setProperty('--background-x', 50 + offsetY / 10 + offsetX + '% ');
        acard.style.setProperty('--posy', 50 + offsetX / 10 + offsetY / 10 + '% ');
        acard.style.setProperty('--background-y', 50 + offsetX / 10 + offsetY / 10 + '% ');
        acard.style.setProperty('--hyp', Pt(Math.sqrt((offsetX - 50) * (offsetX - 50) + (offsetY - 50) * (offsetY - 50)) / 50, 0, 1));
        acard.style.setProperty('--pointer-from-center', Pt(Math.sqrt((offsetX - 50) * (offsetX - 50) + (offsetY - 50) * (offsetY - 50)) / 50, 0, 1));
    }

function initCard(acard)
 {
    acard.style.setProperty('--ry','0deg');
    acard.style.setProperty('--rotate-y','0deg');
    acard.style.setProperty('--rx','0deg');
    acard.style.setProperty('--rotate-x','0deg');
    acard.style.setProperty('--tx','0px');
    acard.style.setProperty('--translate-x','0px');
    acard.style.setProperty('--ty','0px');
    acard.style.setProperty('--translate-y','0px');
    acard.style.setProperty('--s','1');
    acard.style.setProperty('--o','1');
    acard.style.setProperty('--pos','50% 50%');
    acard.style.setProperty('--posx','50%');
    acard.style.setProperty('--background-x','50%');
    acard.style.setProperty('--posy','50%');
    acard.style.setProperty('--background-y','50%');
    acard.style.setProperty('--hyp','0');
    acard.style.setProperty('--pointer-from-center','0');
    CurrentDeg = -180;
 }


function setCardClick() {
    $(function () {
        $('.cardeffect').off('mouseenter').off('mouseleave').off('click');
        $('.cardeffect').on("click", function () {
            if ($(this).hasClass("cardZoom")) {

                $(this).removeClass("cardZoom");
                ZoomCard = null;


            } else {

                $(this).addClass("cardZoom");

                ZoomCard = $(this);

            }
        });
    });
}

function startCardEffects() {
    $(function () {
        // on hover
        $('.cardeffect').off('mouseenter').off('mouseleave').off('click');
        $('.cardeffect').on("mouseenter", function () {

            if (ZoomCard != null) {

                if (!$(this).hasClass("pokecard")) $(this).addClass("pokecard");
                return;

            }

            LastCard = $(this);

            if (!$(this).hasClass("pokecard")) $(this).addClass("pokecard");

            initCard($(this)[0]);

            // on click
            LastCard.on("click", function () {
                if ($(this).hasClass("cardZoom")) {

                    $(this).removeClass("cardZoom");
                    ZoomCard = null;


                } else {

                    $(this).addClass("cardZoom");

                    ZoomCard = $(this);

                }
            });
            LastCard.on("mousemove", function (e) { ShineCard(e); });
            //LastCard.on("mousemove", function (e) {

            //    const force = 10;
            //    const offsetY = -((e.pageY - $(this).offset().top) - $(this).height() / 2) / force;
            //    const offsetX = ((e.pageX - $(this).offset().left) - $(this).width() / 2) / force;

            //    OrientCard(offsetX, offsetY);
            //    OrientCard(offsetX, offsetY);
            //});
        });

        $('.cardeffect').on("mouseleave", function () {

            if (ZoomCard != null) {
                initCard($(this)[0]);
                if ($(this).hasClass("pokecard")) $(this).removeClass("pokecard");
                return;
            }

            if (LastCard != null) {
                // remove event listener
                LastCard.off("mousemove");
                LastCard.off("click");
                if ($(this).hasClass("cardZoom")) $(this).removeClass("cardZoom");
                initCard($(this)[0]);
                if ($(this).hasClass("pokecard")) $(this).removeClass("pokecard");

            }

        });

    });
}

function swapCard(cardObj) {
    cardObj.addEventListener('mousemove', e => {
        OrientCard(e.clientX, e.clientY);
    });

    setTimeout(function () {
        rotate();
    }, 40);
};

function orientationhandler(evt) {

    OrientCard((document.body.clientWidth / 2) + (evt.gamma * 2), (document.body.clientHeight / 2) - evt.beta * 4);

}