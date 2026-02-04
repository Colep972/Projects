// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "Blueprint/UserWidget.h"
#include "LooseMenuWidget.generated.h"

/**
 * 
 */
UCLASS()
class ENTERTHEPOTATO_API ULooseMenuWidget : public UUserWidget
{
	GENERATED_BODY()
	
    public:

        virtual bool Initialize() override;

    protected:

        UPROPERTY(meta = (BindWidget))
        class UButton* ReturnButton;

        UFUNCTION()
        void OnReturnClicked();
};
