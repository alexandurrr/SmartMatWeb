$("#navbar-brand").mouseenter( flipGlasses );
function flipGlasses() {
    $("#logo_glasses").css("animationName", "");
    $("#logo_glasses_container").css("animationName","");
    setTimeout(function(){
        $("#logo_glasses").css("animationName", "flip");
        $("#logo_glasses_container").css("animationName","bounce");
    }, 50)
}