// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//Display preview uploaded images
function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (r) {
            $('#imgPreview').attr('src', r.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    }
}
//////////////////////////////////