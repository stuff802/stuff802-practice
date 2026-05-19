import { UmbModalToken } from '@umbraco-cms/backoffice/modal';

export interface GlossaryEntry {
    title: string;
    url: string;
    anchor: string;
}

export interface GlossaryPickerModalData {
    entries: GlossaryEntry[];
}

export type GlossaryPickerModalValue = GlossaryEntry | undefined;

export const GLOSSARY_PICKER_MODAL = new UmbModalToken<GlossaryPickerModalData, GlossaryPickerModalValue>(
    'Stuff802.Modal.GlossaryPicker',
    {
        modal: {
            type: 'dialog',
            size: 'small',
        },
    }
);
