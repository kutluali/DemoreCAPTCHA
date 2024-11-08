document.addEventListener('DOMContentLoaded', function () {
    let form = document.querySelector('#contactForm');
    if (!form) return;

    form.addEventListener('submit', function (event) {
        event.preventDefault();

        let action = form.getAttribute('action');

        if (!action) {
            displayError('Form action bulunamadý!');
            return;
        }

        // Yükleniyor mesajý göster
        let loadingElement = form.querySelector('.loading');
        let errorElement = form.querySelector('.error-message');
        let successElement = form.querySelector('.sent-message');

        loadingElement.style.display = 'block'; // Yükleniyor kýsmý gösteriliyor
        if (errorElement) errorElement.style.display = 'none';
        if (successElement) successElement.style.display = 'none';

        let formData = new FormData(form);

        // Ajax ile formu gönder
        fetch(action, {
            method: 'POST',
            body: formData,
        })
            .then(response => response.json()) // JSON formatýnda yanýt al
            .then(data => {
                loadingElement.style.display = 'none'; // Yükleniyor mesajý kapat

                if (data.success) {
                    if (successElement) {
                        successElement.innerHTML = data.message; // Baþarý mesajýný göster
                        successElement.style.display = 'block'; // Baþarý mesajý göster
                    }
                    form.reset(); // Formu temizle
                    setTimeout(() => {
                        if (successElement) successElement.style.display = 'none'; // 10 saniye sonra mesajý gizle
                    }, 10000);
                } else {
                    // Hata mesajýný göster
                    displayError(data.errorMessage || 'Mesaj gönderilirken bir hata oluþtu.');
                }
            })
            .catch(error => {
                loadingElement.style.display = 'none'; // Yükleniyor mesajý kapat
                displayError('Mesaj gönderilirken bir hata oluþtu: ' + error);
            });
    });

    function displayError(message) {
        let errorElement = form.querySelector('.error-message');
        if (errorElement) {
            errorElement.innerHTML = message;
            errorElement.style.display = 'block'; // Hata mesajý göster
        }
    }
});