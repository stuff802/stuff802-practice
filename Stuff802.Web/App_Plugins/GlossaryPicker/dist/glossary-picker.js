import { UmbTiptapToolbarElementApiBase as e } from "@umbraco-cms/backoffice/tiptap";
//#region src/index.ts
var t = class extends e {
	async execute(e) {
		if (!e) return;
		e.chain().focus().run();
		let t = await fetch("/api/glossary");
		if (!t.ok) return;
		let n = await t.json();
		console.log("Glossary entries:", n);
	}
};
//#endregion
export { t as default };

//# sourceMappingURL=glossary-picker.js.map