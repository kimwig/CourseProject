// Define arrays for each character type
const symbolChars = "!@#$%^&*()_+~`|}{[]:;?><,./-=\\\'\"";
const numberChars = "0123456789";
const lowercaseChars = "abcdefghijklmnopqrstuvwxyz";
const uppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
const similarChars = "iIlLoO01";

// Get DOM elements
const passwordLength = document.getElementById("passwdLength");
const symbols = document.getElementById("symbols");
const numbers = document.getElementById("numbers");
const lowercase = document.getElementById("lowercase");
const uppercase = document.getElementById("uppercase");
const noSimilar = document.getElementById("noSimilar");
const noAmbiguous = document.getElementById("noAmbiguous");
const generateButton = document.getElementById("generateButton");
const newPassword = document.getElementById("newPasswd");
const copyButton = document.getElementById("copyButton");
const submitButton = document.getElementById("submitButton");
const mediaQuery = window.matchMedia("(min-width: 769px)");

// Generate password
function generatePassword() {
    let password = "";
    let charSet = "";

    // Add character types based on user selection
    if (symbols.checked) {
        charSet += symbolChars;
    }
    if (numbers.checked) {
        charSet += numberChars;
    }
    if (lowercase.checked) {
        charSet += lowercaseChars;
    }
    if (uppercase.checked) {
        charSet += uppercaseChars;
    }
    if (noSimilar.checked) {
        for (let i = 0; i < similarChars.length; i++) {
            charRemove = similarChars[i];
            charSet = charSet.replace(new RegExp(charRemove, "g"), "");
        }
    }

    // Generate password using cryptographically strong random numbers ** DO NOT USE Math.Random FOR GENERATING PASSWORDS **
    for (let i = 0; i < passwordLength.value; i++) {
        let randomNumber = crypto.getRandomValues(new Uint32Array(1))[0];
        randomNumber = randomNumber / 0x100000000;
        randomNumber = Math.floor(randomNumber * charSet.length);

        password += charSet[randomNumber];
    }

    return password;
}

// Event listeners

// Generate Button
generateButton.addEventListener("click", function () {

    // Check if any character sets are selected, if not give error
    if (symbols.checked || numbers.checked || lowercase.checked || uppercase.checked) {
        if (newPassword.style.color !== "") {
            newPassword.style.color = "";
            newPassword.value = generatePassword();
        } else {
            newPassword.value = generatePassword();
        }
    } else if (newPassword.value !== "") {
        newPassword.style.color = "#FF5555"
        newPassword.style.transition = "0.5s ease-in-out";
        if (mediaQuery.matches) {
            newPassword.value = "You must select at least one character set!";
        } else {
            newPassword.value = "Select a character set!";
        }
    } else {
        newPassword.style.color = "#FF5555"
        newPassword.style.transition = "0.5s ease-in-out";
        if (mediaQuery.matches) {
            newPassword.classList.add("placeholder-error");
            newPassword.setAttribute("placeholder", "You must select at least one character set!");
        } else {
            newPassword.classList.add("placeholder-error");
            newPassword.setAttribute("placeholder", "Select a character set!");
        }
    }

});

// Copy Button
copyButton.addEventListener("click", function () {
    newPassword.select();
    navigator.clipboard.writeText(newPassword.value);
});

// Auto select password when clicking in the field
newPassword.addEventListener("click", function () {
    newPassword.select();
});

// Check for when screen size goes above 768px wide and replaces placholder text in new password input
function screenChange(event) {
    if (event.matches) {
        newPassword.setAttribute("placeholder", "Your new password will appear here.");
        submitButton.textContent = ">";
    } else {
        newPassword.setAttribute("placeholder", "Your new password.");
        submitButton.textContent = "Submit";
    }
};

// Screen change event listener
mediaQuery.addEventListener("change", screenChange);
screenChange(mediaQuery);