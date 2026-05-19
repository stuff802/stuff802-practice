import { UmbTiptapToolbarElementApiBase } from '@umbraco-cms/backoffice/tiptap';
import { UMB_MODAL_MANAGER_CONTEXT } from '@umbraco-cms/backoffice/modal';
import { GLOSSARY_PICKER_MODAL } from './glossary-picker-modal.token.js';
import './glossary-picker-modal.element.js';

export default class GlossaryPickerToolbarApi extends UmbTiptapToolbarElementApiBase {

    override async execute(editor?: any) {
        if (!editor) return;

        // Preserve selection before async work
        editor.chain().focus().run();

        const response = await fetch('/umbraco/api/glossary/entries');
        if (!response.ok) return;
        const entries = await response.json();

        const modalManager = await this.getContext(UMB_MODAL_MANAGER_CONTEXT);
        const modal = modalManager.open(this, GLOSSARY_PICKER_MODAL, { data: { entries } });

        const result = await modal.onSubmit().catch(() => undefined);
        if (!result) return;

        const href = result.anchor ? `${result.url}#${result.anchor}` : result.url;
        const { from, to } = editor.state.selection;

        if (from !== to) {
            // Wrap existing selected text in a link
            editor.chain().focus().setLink({ href }).run();
        } else {
            // No selection — insert the term title as a link
            editor.chain().focus()
                .insertContent(`<a href="${href}">${result.title}</a>`)
                .run();
        }
    }
}
