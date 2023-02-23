// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$.ajaxSetup({
  headers: {
    "X-CSRF-TOKEN": $('meta[name="csrf-token"]').attr("content"),
  },
});
$(document).ready(function () {
  //   $("#searchInput").keyup(async function () {
  //     await setTimeout(() => {
  //       $("#form-search").submit();
  //     }, 1500);
  //   });
  $("body").on("click", ".delete-phong", function () {
    var id = $(this).data("id");
    $("#id-delete").val(id);
  });
  $("body").on("click", ".nav-link", function (e) {
    e.preventDefault();
    var id = $(this).data("id");
    $("#form-btn-id").val(id);
    $("#form-btn-dp").submit();
  });
  function checkNav() {
    var nav_phong = $(".nav-btn-p");
    var check_nav = $("#check_nav").val();
    if (check_nav == "") {
        $(nav_phong).first().addClass("active");
    } else {
      jQuery.each(nav_phong, function (index, value) {
        if ($(value).attr("data-id") == check_nav) {
          $(value).addClass("active");
        }
      });
    }
  };
  checkNav();
});
