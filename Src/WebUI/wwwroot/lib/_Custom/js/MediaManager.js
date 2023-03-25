/*
 در این فایل کلیه دستوراتی که برای ذخیره تصویر در زیر دامنه تصاویر لازم است قرار دارد
 
 
 */
var MediaManager = {
    Init: function() {
        InitMediaManager();

    },
    Show :function() {

        return new Promise(function(resolve, reject) {

            $('#media-manager-modal').modal('show');
            $('#copy-img-path').click(function ()
            {
                if (!$('.media-img-card.active').length) {
                    swal('Select Image', 'Please, Select an Image!', 'warning');
                    return;
                }

                var selectedImage   = $('.media-img-card.active').get(0);
                var img             = $(selectedImage).find('img').get(0);
                var src             = $(img).attr('src');

                $('#media-manager-modal').modal('hide');
                resolve
                    ({
                        result: true,
                        imgPath: src,
                    });

            });
            $('#cancel-img').click(function () {
                reject
                    ({
                        result: false,
                        imgPath:""
                    });
            });
        });
    },
};

function InitMediaManager           () {

    DropfyInit();

    $('.caption').click(function (e) {
        MediaManager
            .Show()
            .then(function (e) {
                /*$('#ImageIndexUrl').val(e.imgPath);*/
                $('#logo-preview').attr('src', e.imgPath);
            })
            ;
    });

    $('#cancel-img').click(function (e) {

        e.preventDefault();
        $('#media-manager-modal').modal('hide');
    });
    $('#media-manager-tab').on('shown.bs.tab', function (event) {

        if (event.target.id == 'gallery-tab') {
            if (!$(event.target).hasClass('loaded')) {
                LoadMediaManagerImages();
            }
        }
    });

    $('#media-gallery').on('click', '.media-img-card .btn-img-delete', function (e) {
        e.stopPropagation();

        var $this = $(this);

        swal({
            text: "برای حذف تصویر هستید؟",
            type: "warning",
            buttons: {
                cancel: {
                    text: "انصـراف",
                    value: false,
                    className: "",
                    visible: true,
                    closeModal: true,
                },
                confirm: {
                    text: "حذف شود",
                    value: true,
                    visible: true,
                    className: "",
                    closeModal: true
                }
            }
        }).then(isConfirm => {
             
            if (isConfirm) {
                SendDeleteRequest($this);
            }
        });


    });
    $('#media-gallery').on('click', '.media-img-card', function (e) {
        $('.media-img-card').removeClass('active');
        $(this).addClass('active');
    });

    $('textarea#blog-content').tinymce({

        plugins: 'importcss searchreplace autolink autosave save directionality code visualblocks visualchars image link media table charmap hr pagebreak nonbreaking anchor insertdatetime advlist lists wordcount imagetools noneditable charmap quickbars emoticons',

        imagetools_cors_hosts: ['picsum.photos'],

        menu: {
            edit: {
                title: 'Edit',
                items: 'undo redo | cut copy paste pastetext | selectall | searchreplace'
            },
            view: {
                title: 'View',
                items: 'code | visualaid visualchars visualblocks showcomments'
            },
            insert: {
                title: 'Insert',
                items: 'image link pageembed inserttable | charmap emoticons hr | pagebreak nonbreaking anchor toc '
            },
            format: {
                title: 'Format',
                items: 'bold italic underline strikethrough superscript subscript | formats blockformats fontsizes align | forecolor backcolor | removeformat'
            },
            tools: {
                title: 'Tools',
                items: 'code wordcount'
            },
            table: {
                title: 'Table',
                items: 'inserttable | cell row column | advtablesort | tableprops deletetable'
            }
        },

        menubar: 'edit view insert format tools table',
        toolbar: 'undo redo | bold italic underline strikethrough| image aparat link | fontsizeselect formatselect | alignleft aligncenter alignright alignjustify | outdent indent |  numlist bullist | forecolor backcolor removeformat | pagebreak | charmap emoticons |  anchor | ltr rtl',

        autosave_ask_before_unload: true,
        autosave_interval: '30s',
        autosave_prefix: '{path}{query}-{id}-',
        autosave_restore_when_empty: false,
        autosave_retention: '2m',

        image_advtab: true,
        importcss_append: true,
        file_picker_callback: function (callback, value, meta) {

            if (meta.filetype === 'image') {
                MediaManager
                    .Show()
                    .then(function (e) {
                        callback(e.imgPath, { alt: 'Alt text' });
                    })
                    .catch(function (e) {
                        callback('', { alt: 'Alt text' });
                    });
            }
        },

        quickbars_insert_toolbar: false,
        height: 500,
        image_caption: true,
        quickbars_selection_toolbar: 'bold italic | quicklink h2 h3 blockquote quicktable',
        noneditable_noneditable_class: 'mceNonEditable',
        toolbar_mode: 'sliding',
        skin: 'oxide',
        content_css: 'default',
        content_style: 'body{font-size:14px;}.mce-preview-object {margin:0 auto !important;}'

    });

};
function LoadMediaManagerImages     () {

    $('#spinner-gallery-loading').removeClass('d-none');

    $.ajax({
        dataType: 'json',
        type: 'GET',
        url: '/api/MediaManager',
    })
    .done(function (data) {

        if (data && data.IsSuccess) {
            CreateAlbumList(data.Data); 
        }
        else {
            alert(data.Message);
        }
    })
    .fail(function (data, status, xhr) {
        alert("امکان مشاهده گالری تصاویر به دلیل خطا وجود ندارد");
    })
    .always(function () {
        $('#spinner-gallery-loading').addClass('d-none');
    });


};
function CreateAlbumList            (Data) {

    var gallery = $('#media-gallery');
    gallery.empty();
    /*var url     = $('#media-manager-modal').attr('data-url');*/

    $.each(Data.Files, function (index, img) {

        var template =
`<div class="col-6 col-md-4 col-lg-3 col-xl-2">
    <div class="card media-img-card" data-name="` + img + `" >
        <button class="btn btn-sm btn-danger btn-img-delete">
            <i class="far fa-trash-alt"></i>
        </button>
        <img src="`+ img + `"  class="card-img-top"/>
    </div>
</div>`;

        gallery.append(template);

    });

    $('#gallery-tab').addClass('loaded');

};
function ImageUploadSuccess         (data, status, xhr) {
    if (data.IsSuccess) {

        $('#media-manager-form').trigger("reset");
        $(".dropify-clear").trigger("click");
        $('#save-logo').addClass('d-none');
        ParseMessageSuccess(data);
        LoadMediaManagerImages();

    } else {

        ParseValidationErrors(data);
    }
};
function SendDeleteRequest          ($this) {

    var parent  = $this.closest('.media-img-card');
    var name    = $(parent).attr('data-name');

    $.ajax({
        dataType: 'json',
        type: 'DELETE',
        data: { ImageName : name },
        url: '/api/MediaManager',
    })
        .done(function (data) {

            if (data && data.IsSuccess) {

                LoadMediaManagerImages();
            }
            else {
                alert(data.Message);
            }
        })
        .fail(function (data, status, xhr) {
            alert("امکان حذف تصویر به دلیل خطا وجود ندارد");
        });

};
function DropfyInit() {
    if (!$('.dropify').length)
        return;

    $('.dropify').dropify();

};