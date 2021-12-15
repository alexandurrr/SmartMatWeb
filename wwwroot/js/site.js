
/* LOGO ANIMATION */
$("#navbar-brand").mouseenter( flipGlasses );
let flippingGlasses = false;
function flipGlasses() {
    if (!flippingGlasses){
        flippingGlasses = true;
        $("#logo_glasses").css("animationName", "");
        $("#logo_glasses_container").css("animationName","");
        setTimeout(function(){
            $("#logo_glasses").css("animationName", "flip");
            $("#logo_glasses_container").css("animationName","bounce");
        }, 50)
        setTimeout(function(){
            flippingGlasses = false;
        }, 700)
    }
}

/* Nav-bar Search */
let navSearchBar = document.getElementById("navSearchBar");
let navSearchInput = document.getElementById("navSearchInput");
let navSearchIcon = document.getElementById("navSearchIcon");
let navSearchResult = document.getElementById("navSearchResultContainer");
let navSearchLukk = document.getElementById("navSearchLukk");
let navBarOpen = false;

async function search() {
    let search = $("#navSearchInput").val();
    if (search.length > 1){
        let result = await fetch("/Home/Recipes?search=" + search);
        $("#navSearchResult").html(await result.text());

        navSearchResult.style.display = "block";
        setTimeout(function(){navSearchResult.style.opacity = "1";navSearchResult.style.transform = "translate(-50%, 0)";}, 30)
    }
    else {
        $("#navSearchResult").html("");
        closeSearch();
    }
    openSearch();
}
function openSearch(){
    navSearchInput.focus();
    navBarOpen = true;
    navSearchBar.style.width = "16em";
    navSearchInput.style.opacity = "1";
}
function closeSearch(){
    if (navBarOpen) {
        navBarOpen = false;
        navSearchBar.style.width = "2em";
        navSearchInput.style.opacity = "0";
        navSearchResult.style.opacity = "0";
        navSearchResult.style.transform = "translate(-50%, -4em)";
        setTimeout(function(){navSearchResult.style.display = "none";}, 200);
    }
}

navSearchBar.addEventListener("click", function() {
    if (!navBarOpen) {
        openSearch();
    } 
})
navSearchIcon.addEventListener("click", search)
navSearchInput.onkeyup = search;
document.getElementById("navSearchForm").onsubmit = async function (event){
    event.preventDefault();
    await search();
};
navSearchLukk.addEventListener("click", closeSearch);


/* DARK MODE */
function toggleDarkMode(){
    if (darkModeCheckbox.checked){
        $("body, #navBar").addClass("darkMode");
        $("button, a, label").addClass("darkModeText");
        $(".mainDiv, #search-partial li, .horizontal-scroll a, .reviewBox").addClass("darkModeBorder");
        $("#indexBackground").css({borderRadius: "50%", opacity: "0.7"});
        $(":root").css({globalBackground: "black", globalColor: "black"});
        document.documentElement.style.setProperty('--globalBackground', 'black');
        document.documentElement.style.setProperty('--globalColor', 'white');
        $("#emailIcon").css("filter", "invert()");
    }
    else{
        $("body, button, #navBar").removeClass("darkMode");
        $("a, label").removeClass("darkModeText");
        $("button, .mainDiv, #search-partial li, .horizontal-scroll a, .reviewBox").removeClass("darkModeBorder");
        $("#indexBackground").css({borderRadius: "0%", opacity: "1"});
        document.documentElement.style.setProperty('--globalBackground', 'white');
        document.documentElement.style.setProperty('--globalColor', 'black');
        $("#emailIcon").css("filter", "");
    }
}
$("#darkModeSelector").change(function(){
        let request = new Request("/Home/DarkMode/?darkModeValue="+this.checked, {method: 'POST'});
        fetch(request)
            .then(res => toggleDarkMode());
    });
let darkModeCheckbox = document.getElementById("darkModeSelector"); 
if (darkModeCheckbox){
    fetch("/Home/DarkMode/")
        .then(res => res.text())
        .then(res => darkModeCheckbox.checked = (res === "true"))
        .then(res => toggleDarkMode());
}

setTimeout(function(){
    let css = '#navBar, body, #indexBackground {transition: background-color 500ms, opacity 300ms, color 500ms, border-radius 500ms;} #darkModeSelector, #darkModeSelector:after {-webkit-transition: all .2s ease-in-out; transition: all .2s ease-in-out;} #search-partial ul li {transition: background-color 500ms, opacity 300ms, color 500ms, transform 100ms;}';
    let style = document.createElement('style');
    style.innerText = css;
    document.getElementsByTagName('head')[0].appendChild(style);
}, 300)


/* OTHER */
$("#profileImg").click(function(e) {
    $("#imgUpload").click();
});

function fasterPreview( uploader ) {
    if ( uploader.files && uploader.files[0] ){
        $('#profileImg').attr('src',
            window.URL.createObjectURL(uploader.files[0]) );
    }
}

$(".dropdown-item").click(function() 
{
    $('#visibility').html($(this).text());
    $('#visibility').val($(this).data('value'));
});

$("#imgUpload").change(function(){
    fasterPreview( this );
});