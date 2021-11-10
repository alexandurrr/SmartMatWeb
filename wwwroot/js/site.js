$("#navbar-brand").mouseenter( flipGlasses );
function flipGlasses() {
    $("#logo_glasses").css("animationName", "");
    $("#logo_glasses_container").css("animationName","");
    setTimeout(function(){
        $("#logo_glasses").css("animationName", "flip");
        $("#logo_glasses_container").css("animationName","bounce");
    }, 50)
}


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