document.addEventListener('DOMContentLoaded', function () {
    let form = document.querySelector('#contactForm');
    if (!form) return;

    form.addEventListener('submit', function (event) {
        event.preventDefault();

        let action = form.getAttribute('action');

        if (!action) {
            displayError('Form action bulunamad�!');
            return;
        }

        // Y�kleniyor mesaj� g�ster
        let loadingElement = form.querySelector('.loading');
        let errorElement = form.querySelector('.error-message');
        let successElement = form.querySelector('.sent-message');

        loadingElement.style.display = 'block'; // Y�kleniyor k�sm� g�steriliyor
        if (errorElement) errorElement.style.display = 'none';
        if (successElement) successElement.style.display = 'none';

        let formData = new FormData(form);

        // Ajax ile formu g�nder
        fetch(action, {
            method: 'POST',
            body: formData,
        })
            .then(response => response.json()) // JSON format�nda yan�t al
            .then(data => {
                loadingElement.style.display = 'none'; // Y�kleniyor mesaj� kapat

                if (data.success) {
                    if (successElement) {
                        successElement.innerHTML = data.message; // Ba�ar� mesaj�n� g�ster
                        successElement.style.display = 'block'; // Ba�ar� mesaj� g�ster
                    }
                    form.reset(); // Formu temizle
                    setTimeout(() => {
                        if (successElement) successElement.style.display = 'none'; // 10 saniye sonra mesaj� gizle
                    }, 10000);
                } else {
                    // Hata mesaj�n� g�ster
                    displayError(data.errorMessage || 'Mesaj g�nderilirken bir hata olu�tu.');
                }
            })
            .catch(error => {
                loadingElement.style.display = 'none'; // Y�kleniyor mesaj� kapat
                displayError('Mesaj g�nderilirken bir hata olu�tu: ' + error);
            });
    });

    function displayError(message) {
        let errorElement = form.querySelector('.error-message');
        if (errorElement) {
            errorElement.innerHTML = message;
            errorElement.style.display = 'block'; // Hata mesaj� g�ster
        }
    }
});