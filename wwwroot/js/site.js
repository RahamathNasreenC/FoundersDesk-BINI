/**
 * goToLogin(role)
 * Redirects to the login page with role parameter
 */
"use strict";

function goToLogin(role) {
    if (!role) role = 'intern';
    window.location.href = '/Auth/Login?role=' + encodeURIComponent(role);
}

/**
 * togglePassword(inputId)
 */
function togglePassword(inputId) {
    const el = document.getElementById(inputId);
    if (!el) return;
    el.type = el.type === 'password' ? 'text' : 'password';
}

/**
 * selectRole(role, button)
 */
function selectRole(role, button) {
    document.querySelectorAll('.role-btn').forEach(btn => btn.classList.remove('active'));
    if (button) button.classList.add('active');

    document.querySelectorAll('.video-content').forEach(section => section.classList.add('hidden'));

    const section = document.getElementById(role + '-content');
    if (section) section.classList.remove('hidden');

    if (role === 'general') {
        const firstTab = document.querySelector('.category-tab');
        if (firstTab) showCategory('orientation', firstTab);
    }
}

/**
 * showCategory(category, button)
 */
function showCategory(category, button) {
    document.querySelectorAll('.category-tab').forEach(tab => tab.classList.remove('active'));
    if (button) button.classList.add('active');

    document.querySelectorAll('#general-content .video-grid').forEach(grid => grid.classList.add('hidden'));

    const grid = document.getElementById(category + '-videos');
    if (grid) grid.classList.remove('hidden');
}

/**
 * playVideo(videoUrl)
 */


/**
 * toggleProfileModal()
 */
function toggleProfileModal() {
    const modal = document.getElementById('profileModal');
    if (modal) {
        const isHidden = modal.style.display === 'none' || modal.style.display === '';
        modal.style.display = isHidden ? 'block' : 'none';
    }
}

/**
 * SINGLE INITIALIZATION BLOCK
 */
document.addEventListener('DOMContentLoaded', function () {
    // 1. Initialize Dashboard Video State (Safely)
    const defaultRoleBtn = document.querySelector('.role-btn.active');
    if (defaultRoleBtn) {
        selectRole('general', defaultRoleBtn);
    }

    const videoCards = document.querySelectorAll('.video-card');
    if (videoCards.length > 0) {
        videoCards.forEach(card => {
            card.style.opacity = '1';
            card.style.transform = 'translateY(0)';
            card.style.transition = 'all 0.4s ease';
        });
    }

    // 2. Initialize Profile Dropdown Logic
    const avatar = document.getElementById('profileAvatar');
    const modal = document.getElementById('profileModal');

    if (avatar && modal) {
        avatar.onclick = function (e) {
            e.stopPropagation();
            const isHidden = modal.style.display === 'none' || modal.style.display === '';
            modal.style.display = isHidden ? 'block' : 'none';
        };

        document.addEventListener('click', function (e) {
            if (!modal.contains(e.target) && e.target !== avatar) {
                modal.style.display = 'none';
            }
        });
    }
});
let selectedDocumentId = 0;
function openResource(id, title, path) {
    selectedDocumentId = id;

    // Set title
    document.getElementById("resourceTitle").innerText = title;

    // Set iframe source (PDF or file)
    document.getElementById("resourceFrame").src = path;

    // Reset checkbox
    document.getElementById("ackCheckbox").checked = false;

    // Show modal
    document.getElementById("resourceModal").style.display = "flex";
}


function openResourceFromBtn(btn) {
    openResource(
        btn.dataset.id,
        btn.dataset.title,
        btn.dataset.path
    );
}



function closeResourceModal() {
    document.getElementById('resourceModal').style.display = 'none';
}

function submitAcknowledgement() {
    if (!document.getElementById('ackCheckbox').checked) {
        alert("Please acknowledge before submitting.");
        return;
    }

    fetch('/Resource/Acknowledge', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded'
        },
        body: 'documentId=' + encodeURIComponent(selectedDocumentId)
    })
        .then(res => res.json())
        .then(data => {
            if (data.success) {
                alert("Saved successfully!");
                closeResourceModal();
                location.reload();
            } else {
                alert(data.message || "Failed");
            }
        })
        .catch(err => {
            alert("Server error: " + err);
        });
}

// ===============================
// DIGITAL SIGNATURE FUNCTIONS
// ===============================

function openSignatureModal() {
    document.getElementById("signatureModal").style.display = "flex";
}

function closeSignatureModal() {
    document.getElementById("signatureModal").style.display = "none";
    document.getElementById("signatureFile").value = "";
}

function submitSignature() {
    const fileInput = document.getElementById("signatureFile");

    if (!fileInput.files.length) {
        alert("Please select a signature file.");
        return;
    }

    const formData = new FormData();
    formData.append("signature", fileInput.files[0]);

    fetch("/Staff/UploadSignature", {
        method: "POST",
        body: formData
    })
        .then(res => res.json())
        .then(data => {
            if (data.success) {
                alert("Signature uploaded successfully!");
                closeSignatureModal();
                location.reload(); // refresh status
            } else {
                alert(data.message || "Upload failed.");
            }
        })
        .catch(err => {
            alert("Server error: " + err);
        });
}
function openLearningPlayer(url, title, desc) {
    if (!url) {
        alert("Video not available");
        return;
    }

    const section = document.getElementById("learningPlayerSection");

    // Use class toggle instead of inline style
    section.classList.remove("hidden");

    // Scroll to player
    section.scrollIntoView({ behavior: "smooth" });

    const player = document.getElementById("mainVideoPlayer");
    player.src = url;
    player.load();

    document.getElementById("playerTitle").innerText = title;
    document.getElementById("playerDesc").innerText = desc;
}
function openCourse(courseId) {
    window.location.href = '/Learning/Course/' + courseId;
}






