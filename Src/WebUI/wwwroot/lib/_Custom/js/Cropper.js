var Cropper = {
    Extention   : null,
    CroppeArea  : null,
    CroppeImage : null,

    Init        : function() {

        if (Cropper.CroppeArea == null) {
            BindCropper();
        }

    },
    Bind        : function(image) {


        Cropper.Extention   = SetOutputType(image);
        Cropper.CroppeImage = image;

        },
    Show        : function() {
        return new Promise(function(resolve, reject) {
            
            $("#cropper-modal #cropper-image").attr('src', Cropper.CroppeImage.src);

            $('#cropper-modal').modal('show');


            $('#cropper-modal .modal-dialog .modal-content .modal-footer #cropper-ok').click(function() {
                $('#cropper-image')
                    .croppie('result', { type: 'blob', size: 'viewport', format:Cropper.Extention, quality:1, circle:false })
                    .then(function (blob) {
                         
                        $('#cropper-modal').modal('hide');

                        resolve
                        ({
                            result: true,
                            image: blob
                        });
                    });
            });

            $('#cropper-modal .modal-dialog .modal-content .modal-footer #cropper-cancel').click(function(){
                $('#cropper-modal').modal('hide');

                reject
                ({
                    result:false,
                });
            });

        });
    }
};
function InitCropper() {
    $("#btn-rotate-left").click(function(e) { 

        $('#cropper-image').croppie('rotate',90);
       
    });
    $("#btn-rotate-right").click(function(e) {
        $('#cropper-image').croppie('rotate', -90);

    });

    $('.testCroppie').on('update.croppie', function(ev, cropData) {
        if (cropData.zoom) {
            $("#crop-zoom").val(cropData.zoom);
        }
    });
    $("#crop-zoom").change(function (e) {

        $('#cropper-image').croppie('setZoom', $(this).val());
    });
};
function BindCropper() {

    $('#cropper-modal').on('shown.bs.modal', function() {

        var width = $(window).width();

        if (width < 500) {
            width -= 50;
        } else {
            width = 500
        }

        if (Cropper.CroppeArea == null) {
            Cropper.CroppeArea = $('#cropper-image').croppie({
                viewport: {
                    width: width,
                    height: 270,
                    type: 'square'
                }
                , customClass: 'testCroppie'
                , boundary: { width: width +20  , height: 350}
                , showZoomer: false
                , enableOrientation: true
                //, enableExif :true
            });

            InitCropper();
        }
        
        Cropper
            .CroppeArea.croppie('bind', { url: Cropper.CroppeImage.src })
            .then(function(e) {
                var slider = $('#cropper-modal ')
                    .find('.modal-dialog .modal-content .modal-body .croppie-container .cr-slider-wrap .cr-slider')
                    .get(0);

                if (slider) {

                    var val = $(slider).val();
                    var min = $(slider).attr('min');
                    var max = $(slider).attr('max');

                    $('#crop-zoom').val(val);
                    $('#crop-zoom').attr('min',min);
                    $('#crop-zoom').attr('max',max);

                }
            });


    });
};
function SetOutputType(img) {
    var type = img.src.substring(img.src.indexOf('image/') + 6, img.src.indexOf(';'));
     
    switch (type) {
        case 'jpeg':
        case 'png':
        case 'webp':
            return type;
        case 'gif':
            return 'png';
        default:
            return 'jpeg';
    } 
};

