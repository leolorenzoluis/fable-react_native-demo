module ImagePickerScene

open System
open Fable.Core
open Fable.Import
open Fable.Import.ReactNative
open Fable.Import.ReactNativeImagePicker
open Fable.Helpers.ReactNative
open Fable.Helpers.ReactNative.Props
open Fable.Helpers.ReactNativeImagePicker
open Fable.Helpers.ReactNativeImagePicker.Props

type ImagePickerSceneProperties = {
    initUri : string
    onDone: unit -> unit
}

type ImagePickerState = {
    uri: string
}

type ImagePickerScene (props) as this =
    inherit React.Component<ImagePickerSceneProperties,ImagePickerState>(props)

    do this.state <- { uri = this.props.initUri }

    member x.render () =
        let selectImageButton =
            text [] "Click me to select image!"
            |> touchableHighlight [
                Styles.buttonStyle
                TouchableHighlightProperties.UnderlayColor Styles.touched
                OnPress 
                    (fun () ->
                        (showImagePicker
                            [Title "Image picker"; AllowsEditing true] 
                            (fun result ->    
                                if not result.didCancel then
                                    if String.IsNullOrEmpty result.error then
                                            x.setState { uri = result.uri } )))
            ]

        let doneButton =
            text [ Styles.defaultText ] "Tap me to go back"
            |> touchableHighlight [
                Styles.buttonStyle
                TouchableHighlightProperties.UnderlayColor Styles.touched
                OnPress x.props.onDone]

        let image =
            image 
                [ Source [ Uri x.state.uri; IsStatic true]
                  ImageProperties.Style [
                    ImageStyle.BorderColor "#000000"
                    ImageStyle.Flex 3
                  ]
                ]

        view [ Styles.sceneBackground ]
            [ image
              selectImageButton
              doneButton ]
