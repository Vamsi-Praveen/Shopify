const ICONS_HTML = {
    SUCCESS: '<i class="ph-bold ph-check-circle text-4xl text-green-500"></i>',
    ERROR: '<i class="ph-bold ph-x-circle text-4xl text-red-500"></i>',
    WARNING: '<i class="ph-bold ph-warning-circle text-4xl text-yellow-500"></i>',
    INFO: '<i class="ph-bold ph-info text-4xl text-blue-500"></i>'
};

const ICON_BG_CLASSES = {
    SUCCESS: 'bg-green-100',
    ERROR: 'bg-red-100',
    WARNING: 'bg-yellow-100',
    INFO: 'bg-blue-100'
};

const modal = document.getElementById("modal");
const modalIconContainer = document.getElementById("modal-icon-container");
const modalTitle = document.getElementById("modal-title");
const modalClose = document.getElementById("modal-close");

const modalContent = document.getElementById("modal-content");
const successIcon = `<i class="ph-bold ph-check text-4xl"></i>`;
const errorIcon = `<i class="ph-bold ph-warning-circle text-4xl"></i>`;

function openModal(type, title, content) {
    if (!modal) return;
    const upperType = type.toUpperCase();
    modalIconContainer.innerHTML = ICONS_HTML[upperType] || ICONS_HTML.INFO;
    Object.values(ICON_BG_CLASSES).forEach(cls => modalIconContainer.classList.remove(cls));
    modalIconContainer.classList.add(ICON_BG_CLASSES[upperType] || ICON_BG_CLASSES.INFO);
    modalTitle.textContent = title;
    modalContent.innerHTML = content;
    modal.classList.remove("hidden");
}

function closeModal() {
    modal.classList.add("hidden");
}