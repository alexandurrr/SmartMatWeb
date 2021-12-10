
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


/* DARK MODE */
function toggleDarkMode(){
    if (darkModeCheckbox.checked){
        $("body, #navBar").addClass("darkMode");
        $("a, label").addClass("darkModeText");
        $("button, .mainDiv, #search-partial li, .horizontal-scroll a, .reviewBox").addClass("darkModeBorder");
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
function checkGlutenfree()
{
    var chkBox = document.getElementById('checkGlutenfree')
    if (chkBox.checked)
    {
        return true;
    }
    else
    {
        return false;
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