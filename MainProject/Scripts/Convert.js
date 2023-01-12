function fileValidation(f) {
    var fileInput = f;
    var filepath = fileInput.value;

    var allowedExtensions = /(\.der|\.cer|\.pem)$/i;
    if (!allowedExtensions.exec(filepath)) {
        alert('invalid file type');
        fileInput.value = '';
        return false;
    }
}
function checkSubmit() {
    if (document.getElementById('file').files.length == 0) {
        alert('File cannot be empty');
        return false
    }
}