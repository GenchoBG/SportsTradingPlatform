var current_page = 1;
var max_pages = 0;
var menuBtn;
var menu_is_opened = true;
var navHeight = 0;

$(window).scroll(function() {
    $(".arrow").css("opacity", 1 - $(window).scrollTop() / 250);
});

$(window).on("load", function () {
    navHeight = $("nav").height();
    var height = $(window).height() - navHeight;
    $(".page-wrapper").css("height", height);

    max_pages = $(".page-wrapper").children().length;

    $(".page").bind("mousewheel", scrollToPage());
});

function scrollToPage() {
    return function (e) {
        $(".page").unbind();
        if (e.originalEvent.wheelDelta / 120 > 0) {
            current_page--;
            nextPage();
        } else {
            current_page++;
            nextPage();
        }
    };
}

function nextPage() {
    validate();
    let next_page = "#page" + current_page;

    $('.page-wrapper').animate({
        scrollTop: "+=" + ($(next_page).offset().top - navHeight)
}, 800, 'swing', function () {
        $(".page").bind("mousewheel", scrollToPage());
    });
    console.log(next_page + " " + $(next_page).offset().top);
    console.log('scrolling up !');

}

function validate() {
    if (current_page > max_pages) {
        current_page = max_pages;
    } else if (current_page < 1) {
        current_page = 1;
    }
}