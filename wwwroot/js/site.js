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