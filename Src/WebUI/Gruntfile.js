module.exports = function (grunt) {
    grunt.initConfig({
        clean: ["wwwroot/dist/css/*", "wwwroot/dist/js/*"],

        sass: {
            dist: {
                options: {
                    style: 'expanded'
                },
                files: {
                    "wwwroot/lib/_custom/css/bootstrap-ex.css":
                    "wwwroot/lib/_custom/scss/style.scss"
                }
            }
        },

        uglify:
        {
            js:
            {
                src: [
                    'wwwroot/lib/jquery/jquery.js',
                    'wwwroot/lib/jquery-validation/dist/jquery.validate.js',
                    'wwwroot/lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive.js',
                    'wwwroot/lib/jquery-ajax-unobtrusive-file-upload/jquery.unobtrusive-ajax-ex.js',
                    'wwwroot/jquery-loading-overlay/loadingoverlay.js',

                    'wwwroot/lib/popper.js/umd/popper.js', 
                    'wwwroot/lib/bootstrap/js/bootstrap.js', 
                    'wwwroot/lib/font-awesome/js/all.min.js',
                      
                    'wwwroot/lib/lightbox2/js/lightbox.js',
                    'wwwroot/lib/bootstrap-sweetalert/sweetalert.js',
                    'wwwroot/lib/bootstrap-tagsinput/bootstrap-tagsinput.js',
                    'wwwroot/lib/croppie/croppie.js',
                    'wwwroot/lib/plyr/plyr.js',   
                    'wwwroot/lib/quill/quill.js',
                    'wwwroot/lib/tippy.js/tippy.umd.js',
                    'wwwroot/lib/toastr.js/toastr.min.js', 
                    'wwwroot/lib/waypoints/jquery.waypoints.js', 
                    'wwwroot/lib/waypoints/shortcuts/infinite.js', 
                    'wwwroot/lib/jquery.ns-autogrow/jquery.ns-autogrow.js', 
                    'wwwroot/lib/dropify/js/dropify.js', 
                    'wwwroot/lib/tinymce/tinymce.min.js', 
                    'wwwroot/lib/tinymce/tinymce-jquery/dist/tinymce-jquery.js',  


                    'wwwroot/lib/_custom/js/main.js',
                    'wwwroot/lib/_custom/js/AjaxHelper.js',
                    'wwwroot/lib/_custom/js/common.js',
                    'wwwroot/lib/_custom/js/PostForms.js',
                    'wwwroot/lib/_custom/js/posts.js',
                    'wwwroot/lib/_custom/js/cropper.js',
                    'wwwroot/lib/_custom/js/mediamanager.js'

                ],
                dest: 'wwwroot/dist/js/site.min.js'
            }
        },
        cssmin: {
            options: {
                mergeIntoShorthands: false,
                roundingPrecision: -1
            },
            target: {
                files: {
                    'wwwroot/dist/css/site.min.css':
                    [
                        'wwwroot/lib/_custom/fonts/font.css',
                        'wwwroot/lib/_custom/css/bootstrap-ex.css',
                        'wwwroot/lib/plyr/plyr.css',
                        'wwwroot/lib/lightbox2/css/lightbox.css',
                        'wwwroot/lib/croppie/croppie.css',
                        'wwwroot/lib/toastr.js/toastr.css',
                        'wwwroot/lib/tippy.js/tippy.css',
                        'wwwroot/lib/quill/quill.bubble.css',

                        'wwwroot/lib/bootstrap-sweetalert/sweetalert.css',
                        'wwwroot/lib/bootstrap-tagsinput/bootstrap-tagsinput.css',
                        'wwwroot/lib/dropify/css/dropify.css',
                        'wwwroot/lib/bootstrap-tagsinput/bootstrap-tagsinput.css',

                        'wwwroot/lib/_custom/css/reset.css',
                        'wwwroot/lib/_custom/css/helper.css',

                        'wwwroot/lib/_custom/css/main.css',
                        'wwwroot/lib/_custom/css/header.css',
                        'wwwroot/lib/_custom/css/footer.css',
                        'wwwroot/lib/_custom/css/posts.css',
                        'wwwroot/lib/_custom/css/sidebar.css',
                        'wwwroot/lib/_custom/css/about.css',
                        'wwwroot/lib/_custom/css/mediamanager.css'

                    ]
                }
            }
        } 
       
    });

    grunt.loadNpmTasks("grunt-contrib-clean");
    grunt.loadNpmTasks("grunt-contrib-sass");
    grunt.loadNpmTasks("grunt-contrib-uglify-es");
    grunt.loadNpmTasks("grunt-contrib-cssmin"); 
};