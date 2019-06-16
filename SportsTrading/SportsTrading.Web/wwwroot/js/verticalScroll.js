var current_page = 1;
var max_pages = 0;
var menu_is_opened = true;
var navHeight = 0;
var scrollTop = false;

$(window).on("load", function () {
    $(".fixed-action-btn").append(`<ul>
                                        <li>
                                            <a id="scrollToTop" title="Back to top" class="btn-floating waves-effect waves-light light-blue darken-1" onclick="scrollToHome()" style="opacity: 1">
                                                <i class="material-icons">keyboard_arrow_up</i>
                                            </a>
                                        </li>
                                    </ul>`);

    navHeight = $("nav").height();
    var height = $(window).height() - navHeight;
    $(".page-wrapper").css("height", height);

    $("#scrollToTop").hide();

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
    scrollTopButton();
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

function scrollTopButton() {
    if (current_page === 1) {
        $("#scrollToTop").fadeOut();
        scrollTop = false;
    } else if (!scrollTop) {
        $("#scrollToTop").fadeIn();
        scrollTop = true;
    }
}

function scrollToHome() {
    $(".page").unbind();
    current_page = 1;

    console.log("top");
    $('.page-wrapper').animate({
        scrollTop: $("#page" + current_page).offset().top - navHeight
    }, 1000, 'swing', function () {
        $(".page").bind("mousewheel", scrollToPage());
        });

    scrollTopButton();
}