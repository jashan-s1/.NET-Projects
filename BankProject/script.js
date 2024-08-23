function redirectToLogin() {
    window.location.href = 'login.html'; // Adjust the path to your login page as needed
}

function redirectTodashboard() {
    window.location.href = 'dashboard.html'; // Adjust the path to your login page as needed
}

document.getElementById('loginForm').addEventListener('submit', function(event) {
    event.preventDefault(); // Prevent form from submitting normally

    const username = document.getElementById('username').value;
    const password = document.getElementById('password').value;

    // Send the login data to the backend API
    fetch('https://yourapi.com/api/login', { // Replace with your API endpoint
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ username: username, password: password })
    })
    .then(response => response.json())
    .then(data => {
        if (data.success) {
            // Redirect to the dashboard or home page after successful login
            window.location.href = 'dashboard.html'; // Replace with your dashboard page
        } else {
            // Display error message
            document.getElementById('errorMessage').textContent = 'Invalid username or password';
        }
    })
    .catch(error => {
        console.error('Error:', error);
        document.getElementById('errorMessage').textContent = 'An error occurred. Please try again.';
    });
});
