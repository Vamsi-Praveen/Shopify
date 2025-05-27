const ICONS_HTML = {
    SUCCESS: '<i class="ph-bold ph-check-circle text-4xl text-green-500"></i>',
    ERROR: '<i class="ph-bold ph-x-circle text-4xl text-red-500"></i>',
    DELETE: '<i class="ph-bold ph-x-circle text-4xl text-red-500"></i>',
    WARNING: '<i class="ph-bold ph-warning-circle text-4xl text-yellow-500"></i>',
    INFO: '<i class="ph-bold ph-info text-4xl text-blue-500"></i>'
};

const ICON_BG_CLASSES = {
    SUCCESS: 'bg-green-100',
    ERROR: 'bg-red-100',
    DELETE: 'bg-red-100',
    WARNING: 'bg-yellow-100',
    INFO: 'bg-blue-100'
};

const modal = document.getElementById("modal");
const modalIconContainer = document.getElementById("modal-icon-container");
const modalTitle = document.getElementById("modal-title");
const modalClose = document.getElementById("modal-close");
const modalBtnContainer = document.getElementById("modal-btn");

const modalContent = document.getElementById("modal-content");
const successIcon = `<i class="ph-bold ph-check text-4xl"></i>`;
const errorIcon = `<i class="ph-bold ph-warning-circle text-4xl"></i>`;

function openModal(type, title, content,callback) {
    if (!modal) return;
    const upperType = type.toUpperCase();
    modalIconContainer.innerHTML = ICONS_HTML[upperType] || ICONS_HTML.INFO;
    Object.values(ICON_BG_CLASSES).forEach(cls => modalIconContainer.classList.remove(cls));
    modalIconContainer.classList.add(ICON_BG_CLASSES[upperType] || ICON_BG_CLASSES.INFO);

    modalTitle.textContent = title;
    modalContent.innerHTML = content;

    modalBtnContainer.innerHTML = "";
    if (upperType === "DELETE") {
        const btn = document.createElement("button");
        btn.className =
            "px-4 py-2 text-sm font-medium text-white bg-red-600 border border-transparent " +
            "rounded-sm cursor-pointer shadow-sm hover:bg-red-700 flex items-center gap-2";
        btn.innerHTML = '<i class="ph ph-trash text-lg"></i> Confirm Delete';

        if (typeof callback === "function") {
            btn.addEventListener("click", () => {
                callback();         
                closeModal();
            });
        }
        modalBtnContainer.appendChild(btn);
    }
   
    modal.classList.remove("hidden");
}



function closeModal() {
    modal.classList.add("hidden");
}