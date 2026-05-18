import { UmbTiptapToolbarElementApiBase } from '@umbraco-cms/backoffice/tiptap';

export default class GlossaryPickerToolbarApi extends UmbTiptapToolbarElementApiBase {
    override async execute(editor?: any) {
        if (!editor) return;

        // Save the current selection before opening the modal
        editor.chain().focus().run();

        // Fetch glossary entries from our API
        const response = await fetch('/api/glossary');
        if (!response.ok) return;

        const entries = await response.json();

        // We'll replace this with a proper modal shortly
        console.log('Glossary entries:', entries);
    }
}
