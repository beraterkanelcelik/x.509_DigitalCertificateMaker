var startDate;
var endDate;

function insertStartDate(f) {
    startDate = f.value;
    checkDates()
    checkstartDate()
}
function insertEndDate(f) {
    endDate = f.value;
    checkDates()
}
function checkDates() {
    if (startDate != null && endDate != null) {
        if (startDate > endDate) {
            alert("To Date cannot be earlier than From Date!");
        }
    }

}
function checkstartDate() {
    var currentdate = new Date();
    var test = new Date(startDate);
    if (test > currentdate) {
        alert("From Date cannot be later than now!");
    }
}