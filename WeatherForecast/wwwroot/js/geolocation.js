if (navigator.geolocation) {
    navigator.geolocation.getCurrentPosition(function (position) {
        let lat = position.coords.latitude;
        let lon = position.coords.longitude;

        document.location.href = `/${lat},${lon}`;
    });
}