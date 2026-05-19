import { UmbTiptapToolbarElementApiBase as e } from "@umbraco-cms/backoffice/tiptap";
//#region src/index.ts
var t = class extends e {
	async execute(e) {
		if (!e) return;
		e.chain().focus().run();
		let t = await fetch("/umbraco/api/glossary/entries");
		if (!t.ok) return;
		let n = await t.json(), r = await this._showPicker(n);
		if (!r) return;
		let i = r.anchor ? `${r.url}#${r.anchor}` : r.url, { from: a, to: o } = e.state.selection;
		a === o ? e.chain().focus().insertContent(`<a href="${i}">${r.title}</a>`).run() : e.chain().focus().setLink({ href: i }).run();
	}
	_showPicker(e) {
		return new Promise((t) => {
			let n = document.createElement("dialog");
			Object.assign(n.style, {
				padding: "0",
				border: "none",
				borderRadius: "6px",
				boxShadow: "0 8px 32px rgba(0,0,0,0.3)",
				width: "480px",
				maxWidth: "90vw",
				maxHeight: "80vh",
				overflow: "hidden",
				display: "flex",
				flexDirection: "column",
				fontFamily: "sans-serif"
			}), n.innerHTML = "\n                <div style=\"padding:16px 20px 12px;border-bottom:1px solid #e0e0e0;background:#f8f8f8;\">\n                    <p style=\"margin:0 0 10px;font-size:15px;font-weight:600;color:#333;\">Insert Glossary Link</p>\n                    <input id=\"gloss-search\" type=\"search\" placeholder=\"Search terms…\"\n                        style=\"width:100%;padding:7px 10px;border:1px solid #ccc;border-radius:4px;font-size:14px;box-sizing:border-box;\" />\n                </div>\n                <div id=\"gloss-list\" style=\"overflow-y:auto;flex:1;max-height:380px;\"></div>\n                <div style=\"padding:10px 20px;border-top:1px solid #e0e0e0;text-align:right;background:#f8f8f8;\">\n                    <button id=\"gloss-cancel\"\n                        style=\"padding:6px 16px;border:1px solid #ccc;border-radius:4px;background:#fff;cursor:pointer;font-size:14px;\">\n                        Cancel\n                    </button>\n                </div>\n            ";
			let r = n.querySelector("#gloss-list"), i = n.querySelector("#gloss-search"), a = (n) => {
				let i = e.filter((e) => e.title.toLowerCase().includes(n.toLowerCase()));
				if (i.length === 0) {
					r.innerHTML = "<p style=\"padding:16px;color:#888;text-align:center;\">No matching terms</p>";
					return;
				}
				r.innerHTML = i.map((t) => `<div data-i="${e.indexOf(t)}"
                        style="padding:9px 20px;cursor:pointer;border-bottom:1px solid #f0f0f0;font-size:14px;color:#333;">
                        ${t.title}
                    </div>`).join(""), r.querySelectorAll("div[data-i]").forEach((n) => {
					n.addEventListener("mouseenter", () => n.style.background = "#f0f5ff"), n.addEventListener("mouseleave", () => n.style.background = ""), n.addEventListener("click", () => {
						t(e[Number(n.dataset.i)]), o();
					});
				});
			}, o = () => {
				n.close(), document.body.removeChild(n);
			};
			a(""), i.addEventListener("input", () => a(i.value)), n.querySelector("#gloss-cancel").addEventListener("click", () => {
				t(void 0), o();
			}), n.addEventListener("close", () => {
				t(void 0), n.parentNode && document.body.removeChild(n);
			}), document.body.appendChild(n), n.showModal(), i.focus();
		});
	}
};
//#endregion
export { t as default };

//# sourceMappingURL=glossary-picker.js.map