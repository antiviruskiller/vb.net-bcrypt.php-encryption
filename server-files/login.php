<?php
// login.php
header('Content-Type: text/plain');

// Enable error reporting for debugging (disable in production)
error_reporting(E_ALL);
ini_set('display_errors', 1);

// Database connection details
$servername = "localhost";
$username = "USERNAME"; // Replace with your database username
$password = "YOUR PASSWORD"; // Replace with your database password
$dbname = "user_db_CREATED";

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

// Retrieve and sanitize input
$user = isset($_POST['username']) ? $conn->real_escape_string($_POST['username']) : '';
$pass = isset($_POST['password']) ? $_POST['password'] : '';

// Check if username and password are provided
if (empty($user) || empty($pass)) {
    echo "Username and password are required.";
    $conn->close();
    exit();
}

// Query to check if user exists
$sql = "SELECT password FROM users WHERE username = ?";
$stmt = $conn->prepare($sql);

if ($stmt === FALSE) {
    echo "Error preparing statement: " . $conn->error;
    $conn->close();
    exit();
}

$stmt->bind_param("s", $user);
$stmt->execute();
$result = $stmt->get_result();

if ($result->num_rows > 0) {
    // User exists, now verify password
    $row = $result->fetch_assoc();
    $hashedPassword = $row['password'];
    
    if (password_verify($pass, $hashedPassword)) {
        echo "Login successful";
    } else {
        echo "Invalid username or password";
    }
} else {
    echo "Invalid username or password";
}

$stmt->close();
$conn->close();
?>
